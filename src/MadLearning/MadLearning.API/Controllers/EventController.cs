using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Persistence;
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
        public async Task<List<GetEventModelApiDto>> GetEvents([FromQuery] EventFilterApiDto eventFilter, CancellationToken cancellationToken)
        {
            var events = await this.eventRepository.GetEvents(eventFilter, cancellationToken);

            return events;
        }

        [HttpGet("{eventId}")]
        public async Task<GetEventModelApiDto?> GetEvent(string eventId, CancellationToken cancellationToken)
        {
            return await this.eventRepository.GetEvent(eventId, cancellationToken);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventModelApiDto eventModel, CancellationToken cancellationToken)
        {
            var dto = await this.eventRepository.CreateEvent(eventModel, cancellationToken);
            return this.CreatedAtAction(nameof(this.GetEvent), new { eventId = dto.Id }, dto);
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
