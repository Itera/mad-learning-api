using MadLearning.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Service
{
    public interface ICalendarService
    {
        Task AddEvent(EventModel eventModel);
    }
}
