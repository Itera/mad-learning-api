using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Service
{
    public class CalendarException : Exception
    {
        public CalendarException()
        {
        }

        public CalendarException(string message)
            : base(message)
        {
        }

        public CalendarException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
