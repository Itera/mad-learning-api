using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Dtos
{
    public record RSVPToEventModelApiDto(
        IEnumerable<PersonModelApiDto>? Participants);
}
