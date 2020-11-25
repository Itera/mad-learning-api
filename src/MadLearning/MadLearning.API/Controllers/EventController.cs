using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Events.Commands;
using MadLearning.API.Application.Events.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public sealed class EventController : ApiControllerBase
    {
        private readonly ITokenAcquisition tokenAcquisition;
        private IConfiguration configuration;

        public EventController(IConfiguration configuration, ITokenAcquisition tokenAcquisition)
        {
            this.configuration = configuration;
            this.tokenAcquisition = tokenAcquisition;
        }

        [HttpGet("{eventId}")]
        public async Task<GetEventModelApiDto?> GetEvent(string eventId, CancellationToken cancellationToken)
        {
            return await this.Mediator.Send(new GetEvent(eventId), cancellationToken);
        }

        [HttpGet]
        public async Task<List<GetEventModelApiDto>> GetEvents([FromQuery] EventFilterApiDto eventFilter, CancellationToken cancellationToken)
        {
            return await this.Mediator.Send(new GetEvents(eventFilter), cancellationToken);
        }

        [HttpPost]
        public async Task<GetEventModelApiDto?> CreateEvent([FromBody] CreateEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            this.HttpContext.VerifyUserHasAnyAcceptedScope(this.configuration["ApiScope"]);

            return await this.Mediator.Send(new CreateEvent(eventModel), cancellationToken);
        }

        [HttpPut]
        public async Task UpdateEvent([FromBody] UpdateEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            await this.Mediator.Send(new UpdateEvent(eventModel), cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteEvent([FromBody] DeleteEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            await this.Mediator.Send(new DeleteEvent(eventModel), cancellationToken);
        }
    }
}
