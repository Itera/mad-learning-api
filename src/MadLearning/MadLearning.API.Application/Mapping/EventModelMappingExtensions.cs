using MadLearning.API.Application.Dtos;
using MadLearning.API.Domain.Entities;
using System;
using System.Linq;

namespace MadLearning.API.Application.Mapping
{
    public static class EventModelMappingExtensions
    {
        public static EventModel ToEventModel(this EventModelDbDto? dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.Id is null)
                throw new InvalidOperationException("Event can only be created from valid DB dto");
            if (dto.Name is null)
                throw new InvalidOperationException("Event can only be created from valid DB dto");
            if (dto.Description is null)
                throw new InvalidOperationException("Event can only be created from valid DB dto");

            return new EventModel(
                dto.Id,
                dto.Name,
                dto.Description,
                dto.Time,
                dto.ImageUrl,
                dto.Owner.ToPersonModel(),
                dto.Participants.Select(d => d.ToPersonModel()));
        }
    }
}
