using MadLearning.API.Application.Service;
using MadLearning.API.Domain.Entities;
using Microsoft.Graph;
using Microsoft.Graph.Extensions;
using System;
using System.Threading.Tasks;

namespace MadLearning.API.Infrastructure.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly GraphServiceClient graphServiceClient;

        public CalendarService(GraphServiceClient graphServiceClient)
        {
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
            catch (Exception e)
            {
                throw new CalendarException(e.Message, e);
            }
        }
    }
}
