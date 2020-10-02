using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Extensions;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Jobs
{
    internal sealed class ChatMessageEventNotificationJob : IJob
    {
        private readonly IEventRepository eventRepository;
        private readonly IChatMessageService chatMessageService;

        public ChatMessageEventNotificationJob(IEventRepository eventRepository, IChatMessageService chatMessageService)
        {
            this.eventRepository = eventRepository;
            this.chatMessageService = chatMessageService;
        }

        public async Task Execute()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            var cancellationToken = cts.Token;

            var tomorrow = DateTimeOffset.UtcNow.AddDays(1);
            var startOfTomorrow = tomorrow.TruncateTime();

            var filter = new EventFilterApiDto
            {
                From = startOfTomorrow,
                To = startOfTomorrow.AddDays(1).AddSeconds(-1),
            };
            var events = await this.eventRepository.GetEvents(filter, cancellationToken);

            var names = events
                .Select(e => $"• {e.StartTime:HH:mm}-{e.EndTime:HH:mm} - <https://vg.no|{e.Name}> at {e.Location}") // TODO fix link
                .ToArray();

            var message = $"*Here are tomorrow's events:* \n{string.Join('\n', names)}";

            await this.chatMessageService.SendMessage(message, cancellationToken);
        }
    }
}
