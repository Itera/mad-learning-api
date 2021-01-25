using System;

namespace MadLearning.API.Domain.Entities
{
    public enum EventType
    {
        SubjectMatterEvent,
        CodingEvent,
        ProjectPresentation,
        Workshop,
    }

    public static class EventTypeUtil
    {
        public static EventType Parse(string eventTypeStr) =>
            eventTypeStr switch
            {
                "Subject matter event" => EventType.SubjectMatterEvent,
                "Coding event" => EventType.CodingEvent,
                "Project presentation" => EventType.ProjectPresentation,
                "Workshop" => EventType.Workshop,
                _ => throw new ArgumentException("Invalid value for eventtype: " + eventTypeStr, nameof(eventTypeStr)),
            };

        public static string ToName(this EventType eventType) =>
            eventType switch
            {
                EventType.SubjectMatterEvent => "Subject matter event",
                EventType.CodingEvent => "Coding event",
                EventType.ProjectPresentation => "Project presentation",
                EventType.Workshop => "Workshop",
                _ => throw new ArgumentException("Invalid value for eventtype: " + eventType, nameof(eventType)),
            };
    }
}
