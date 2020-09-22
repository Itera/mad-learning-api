using MadLearning.API.Model;
using MadLearning.API.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MadLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository eventService;

        public EventController(IEventRepository eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<List<EventModel>> GetEvent([FromQuery] EventFilter eventFilter)
        {
            var events = await eventService.GetEvents(eventFilter);

            return events;
        }

        [HttpPost]
        public async Task CreateEvent([FromBody] EventModel eventModel)
        {
            await eventService.CreateEvent(eventModel);        
        }

        [HttpPut]
        public async Task UpdateEvent([FromBody] EventModel eventModel)
        {
            await eventService.UpdateEvent(eventModel);
        }

        [HttpDelete]
        public async Task DeleteEvent([FromBody] EventModel eventModel)
        { 
            await eventService.DeleteEvent(eventModel);
        }
    }
}
