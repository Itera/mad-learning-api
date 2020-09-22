﻿using MadLearning.API.Models;
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
        private readonly IEventRepository eventService;

        public EventController(IEventRepository eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<List<EventModel>> GetEvent([FromQuery] EventFilter eventFilter, CancellationToken cancellationToken)
        {
            var events = await eventService.GetEvents(eventFilter, cancellationToken);

            return events;
        }

        [HttpPost]
        public async Task CreateEvent([FromBody] EventModel eventModel, CancellationToken cancellationToken)
        {
            await eventService.CreateEvent(eventModel, cancellationToken);        
        }

        [HttpPut]
        public async Task UpdateEvent([FromBody] EventModel eventModel, CancellationToken cancellationToken)
        {
            await eventService.UpdateEvent(eventModel, cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteEvent([FromBody] EventModel eventModel, CancellationToken cancellationToken)
        { 
            await eventService.DeleteEvent(eventModel, cancellationToken);
        }
    }
}
