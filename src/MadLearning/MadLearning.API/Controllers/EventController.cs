using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Events.Commands;
using MadLearning.API.Application.Events.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            //this.HttpContext.VerifyUserHasAnyAcceptedScope(this.configuration["ApiScope"]);

            return await this.Mediator.Send(new CreateEvent(eventModel), cancellationToken);
        }

        [HttpPut]
        public async Task UpdateEvent([FromBody] UpdateEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            await this.Mediator.Send(new UpdateEvent(eventModel), cancellationToken);
        }

        [HttpDelete("{eventId}")]
        public async Task DeleteEvent(string eventId, CancellationToken cancellationToken)
        {
            await this.Mediator.Send(new DeleteEvent(eventId), cancellationToken);
        }

        [HttpPut("{eventId}")]
        public async Task RSVPToEvent(string eventId, CancellationToken cancellationToken)
        {
            await this.Mediator.Send(new RSVPToEvent(eventId), cancellationToken);
        }
    }
}
