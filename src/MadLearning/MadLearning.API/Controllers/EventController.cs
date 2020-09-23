using MadLearning.API.Dtos;
using MadLearning.API.Models;
using MadLearning.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository eventRepository;

        public EventController(IEventRepository eventService)
        {
            this.eventRepository = eventService;
        }

        [HttpGet]
        public async Task<List<GetEventModelApiDto>> GetEvent([FromQuery] EventFilter eventFilter, CancellationToken cancellationToken)
        {
            var events = await this.eventRepository.GetEvents(eventFilter, cancellationToken);

            return events;
        }

        [HttpPost]
        public async Task CreateEvent([FromBody] CreateEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            await this.eventRepository.CreateEvent(eventModel, cancellationToken);
        }

        [HttpPut]
        public async Task UpdateEvent([FromBody] UpdateEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            await this.eventRepository.UpdateEvent(eventModel, cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteEvent([FromBody] DeleteEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            await this.eventRepository.DeleteEvent(eventModel, cancellationToken);
        }
    }
}
