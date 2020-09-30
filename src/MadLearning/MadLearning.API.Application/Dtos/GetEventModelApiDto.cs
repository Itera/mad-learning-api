﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MadLearning.API.Application.Dtos
{
    public record GetEventModelApiDto(string Id, string Name, string Description, DateTimeOffset Time, string? ImageUrl, PersonModelApiDto? Owner, IEnumerable<PersonModelApiDto>? Participants);
}
