using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MadLearning.API.Application.Service;
using MadLearning.API.Domain.Entities;
using Microsoft.Graph;
using Microsoft.Graph.Extensions;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace MadLearning.API.Infrastructure.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ITokenAcquisition tokenAcquisition;
        private readonly GraphServiceClient graphServiceClient;

        public CalendarService(ITokenAcquisition tokenAcquisition, GraphServiceClient graphServiceClient)
        {
            this.tokenAcquisition = tokenAcquisition;
            this.graphServiceClient = graphServiceClient;
        }

        public async Task AddEvent(EventModel eventModel)
        {
            var @event = new Event
            {
                Subject = eventModel.Name,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = eventModel.Description,
                },
                Start = eventModel.StartTime.ToDateTimeTimeZone(),
                End = eventModel.EndTime.ToDateTimeTimeZone(),
            };

            try
            {
                await this.graphServiceClient.Me.Events.Request().AddAsync(@event);
            }
            catch (ServiceException e)
            {
                throw new CalendarException(e.Message, e);
            }
        }
    }
}
