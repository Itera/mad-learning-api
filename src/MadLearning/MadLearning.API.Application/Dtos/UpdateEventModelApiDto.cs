﻿using System;
using System.Collections.Generic;

namespace MadLearning.API.Application.Dtos
{
    public record UpdateEventModelApiDto(string Id, string Name, string Description, DateTimeOffset Time, PersonModelApiDto? Owner, IEnumerable<PersonModelApiDto>? Participants);
}
