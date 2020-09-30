using MadLearning.API.Domain.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MadLearning.API.Infrastructure.Persistence
{
    internal sealed partial class EventRepository
    {
        public sealed class PersonModelDbDto
        {
            public string? Id { get; init; }

            public string? FirstName { get; set; }

            public string? LastName { get; set; }

            public string? Email { get; set; }

            public PersonModel ToPersonModel()
            {
                if (this.Id is null)
                    throw new InvalidOperationException("Event can only be created from valid DB dto");
                if (this.FirstName is null)
                    throw new InvalidOperationException("Event can only be created from valid DB dto");
                if (this.LastName is null)
                    throw new InvalidOperationException("Event can only be created from valid DB dto");
                if (this.Email is null)
                    throw new InvalidOperationException("Event can only be created from valid DB dto");

                return new PersonModel(this.Id, this.FirstName, this.LastName, this.Email);
            }
        }
    }

    internal static class PersonModelDbDtoMappingExtensions
    {
        public static EventRepository.PersonModelDbDto? ToDbDto(this PersonModel? dto)
        {
            if (dto is null)
                return null;

            return new EventRepository.PersonModelDbDto
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
            };
        }

        public static IEnumerable<EventRepository.PersonModelDbDto>? ToDbDtos(this IEnumerable<PersonModel>? dtos)
        {
            return dtos?.Select(d => d.ToDbDto()!);
        }
    }
}
