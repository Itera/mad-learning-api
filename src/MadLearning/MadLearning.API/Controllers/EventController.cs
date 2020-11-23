﻿using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Events.Commands;
using MadLearning.API.Application.Events.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
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
            //this.HttpContext.VerifyUserHasAnyAcceptedScope(this.configuration["ApiScope"]);

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

        [HttpPut("{eventId}")]
        public async Task RSVPToEvent([FromQuery] string eventId, CancellationToken cancellationToken)
        {
            if (this.HttpContext.User == null || this.HttpContext.User.Identity?.Name == null)
            {
                throw new Exception("HttpContext nullable error");
            }

            var email = this.HttpContext.User.Identity.Name;
            var firstName = this.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.GivenName).Value;
            var lastName = this.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Surname).Value;
            await this.Mediator.Send(new RSVPToEvent(eventId, email, firstName, lastName), cancellationToken);
        }
    }
}
