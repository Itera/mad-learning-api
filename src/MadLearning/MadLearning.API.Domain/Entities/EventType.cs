using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLearning.API.Domain.Entities
{
    public enum EventType
    {
        SubjectMatterEvent,
        CodingEvent,
        ProjectPresentation,
        Workshop,
    }

    public static class ToEventTypeEnum
    {
        public static EventType ToEventTypeNum(string eventType)
        {
            return eventType switch
            {
                "Subject matter event" => EventType.SubjectMatterEvent,
                "Coding event" => EventType.CodingEvent,
                "Project presentation" => EventType.ProjectPresentation,
                "Workshop" => EventType.Workshop,
                _ => throw new Exception(),
            };
        }
    }
}
