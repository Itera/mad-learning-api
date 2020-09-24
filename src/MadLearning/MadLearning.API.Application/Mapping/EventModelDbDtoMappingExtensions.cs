using MadLearning.API.Application.Dtos;
using MadLearning.API.Domain.Entities;
using System;
using System.Linq;

namespace MadLearning.API.Application.Mapping
{
    public static class EventModelDbDtoMappingExtensions
    {
        public static EventModelDbDto ToDbDto(this EventModel? model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            return new EventModelDbDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Owner = model.Owner.ToDbDto(),
                Participants = model.Participants.ToDbDtos()?.ToList() ?? new (),
                Time = model.Time,
            };
        }
    }
}
