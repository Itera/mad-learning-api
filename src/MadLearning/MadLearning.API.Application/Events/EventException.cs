using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events
{
    public class EventException : Exception
    {
        public EventException()
        {
        }

        public EventException(string message)
            : base(message)
        {
        }

        public EventException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
