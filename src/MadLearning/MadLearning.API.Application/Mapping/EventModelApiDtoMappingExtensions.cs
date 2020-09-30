using MadLearning.API.Application.Dtos;
using MadLearning.API.Domain.Entities;
using System;

namespace MadLearning.API.Application.Mapping
{
    public static class EventModelApiDtoMappingExtensions
    {
        public static GetEventModelApiDto ToApiDto(this EventModel? model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            return new GetEventModelApiDto(
                model.Id,
                model.Name,
                model.Description,
                model.Time,
                model.ImageUrl,
                model.Owner.ToApiDto(),
                model.Participants.ToApiDtos());
        }
    }
}
