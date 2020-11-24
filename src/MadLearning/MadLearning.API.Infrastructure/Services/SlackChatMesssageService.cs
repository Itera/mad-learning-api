using DnsClient.Internal;
using MadLearning.API.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Infrastructure.Services
{
    public sealed record SlackOptions
    {
        public string? WebHookUrl { get; init; }
    }

    internal sealed class SlackChatMesssageService : IChatMessageService
    {
        private const string ContentType = "application/json";

        private readonly ILogger<SlackChatMesssageService> logger;
        private readonly HttpClient? httpClient;

        public SlackChatMesssageService(ILogger<SlackChatMesssageService> logger, HttpClient httpClient, IOptions<SlackOptions> options)
        {
            this.logger = logger;

            if (!string.IsNullOrWhiteSpace(options.Value.WebHookUrl) && options.Value.WebHookUrl != "<url>")
            {
                httpClient.BaseAddress = new Uri(options.Value.WebHookUrl ?? throw new Exception("No webhook URL configured"));
                httpClient.DefaultRequestHeaders.Add("Accept", ContentType);

                this.httpClient = httpClient;
            }
        }

        public async Task SendMessage(string message, CancellationToken cancellationToken)
        {
            if (this.httpClient is null)
            {
                this.logger.LogInformation("Slack integration not configured, would send: {message}", message);
                return;
            }

            message = message.Replace('"', '\'');

            var responseContent = string.Empty;
            try
            {
                using var content = new StringContent($"{{ \"text\": \"{message}\" }}", Encoding.UTF8, ContentType);

                using var response = await this.httpClient.PostAsync(string.Empty, content, cancellationToken);

                responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error sending Slack message. Response: {responseContent}", responseContent);
                throw new ChatMessageServiceException("Failed to send chat message", ex);
            }
        }
    }
}
