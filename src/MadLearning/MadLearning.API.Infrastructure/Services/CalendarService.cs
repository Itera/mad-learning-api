using MadLearning.API.Application.Services;
using MadLearning.API.Domain.Entities;
using Microsoft.Graph;
using Microsoft.Graph.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Infrastructure.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly GraphServiceClient graphServiceClient;
        private readonly ICurrentUserService currentUserService;

        public CalendarService(GraphServiceClient graphServiceClient, ICurrentUserService currentUserService)
        {
            this.graphServiceClient = graphServiceClient;
            this.currentUserService = currentUserService;
        }

        public async Task<(string EventId, string EventUid)> AddEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            var @event = ToCalendarEvent(eventModel);

            try
            {
                var calendarEvent = await this.graphServiceClient.Me.Events.Request().AddAsync(@event, cancellationToken);
                return (calendarEvent.Id, calendarEvent.ICalUId);
            }
            catch (Exception e)
            {
                throw new CalendarException(e.Message, e);
            }
        }

        public async Task RsvpToEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            var eventId = eventModel.CalendarId;
            var eventUid = eventModel.CalendarUid;

            try
            {
                if (eventModel.Owner is null)
                    throw new Exception("Event has no owner");

                var currentUser = this.currentUserService.GetUserInfo();

                var ownEvents = await this.graphServiceClient.Me.Events.Request().Filter($"iCalUId eq '{eventUid}'").GetAsync(cancellationToken);
                if (ownEvents.Count != 1)
                    throw new Exception("Couldn't find event to RSVP");

                var events = this.graphServiceClient.Users[eventModel.Owner.Email].Events[eventId];

                var request = events.Accept(SendResponse: true).Request();

                await request.PostAsync(cancellationToken);

                //var existingEvent = await events.Request().GetAsync(cancellationToken);

                //if (existingEvent.Attendees?.Any(a => a.EmailAddress.Address == currentUser.Email) ?? false)
                //    return;

                //// TODO: not concurrency safe...

                //var attendees = existingEvent.Attendees?.ToList() ?? new ();
                //attendees.Add(new Attendee
                //{
                //    EmailAddress = new EmailAddress
                //    {
                //        Address = currentUser.Email,
                //        Name = currentUser.FullName,
                //    },
                //    Type = AttendeeType.Optional,
                //});

                //var @event = new Event
                //{
                //    Id = eventId,
                //    Attendees = attendees,
                //};

                //await events.Request().UpdateAsync(@event, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CalendarException(e.Message, e);
            }
        }

        private static Event ToCalendarEvent(EventModel eventModel)
        {
            var @event = new Event
            {
                Subject = eventModel.Name,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = $"<h3>{eventModel.Name}</h3><p>{eventModel.Description}<p>",
                },
                IsOnlineMeeting = true,
                OnlineMeetingProvider = OnlineMeetingProviderType.TeamsForBusiness,
                Start = eventModel.StartTime.ToDateTimeTimeZone(),
                End = eventModel.EndTime.ToDateTimeTimeZone(),
            };

            if (!string.IsNullOrWhiteSpace(eventModel.Location))
            {
                @event.Location = new Location
                {
                    DisplayName = eventModel.Location,
                };
            }

            return @event;
        }
    }
}
