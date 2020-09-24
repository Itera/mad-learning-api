using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Common.Behaviours
{
    public sealed record UnhandledExceptionBehaviour<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                // TODO: log CorrelationId etc for better observability
                this.logger.LogError(ex, "Unhandled Exception for Request {Name} {@Request}", requestName, request);

                throw;
            }
        }
    }
}
