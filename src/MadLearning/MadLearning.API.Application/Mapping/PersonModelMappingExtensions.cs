using MadLearning.API.Application.Dtos;
using MadLearning.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MadLearning.API.Application.Mapping
{
    public static class PersonModelMappingExtensions
    {
        public static PersonModel ToPersonModel(this PersonModelDbDto? dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.Id is null)
                throw new InvalidOperationException("Event can only be created from valid DB dto");
            if (dto.FirstName is null)
                throw new InvalidOperationException("Event can only be created from valid DB dto");
            if (dto.LastName is null)
                throw new InvalidOperationException("Event can only be created from valid DB dto");
            if (dto.Email is null)
                throw new InvalidOperationException("Event can only be created from valid DB dto");

            return new PersonModel(dto.Id, dto.FirstName, dto.LastName, dto.Email);
        }

        public static PersonModel? ToPersonModel(this PersonModelApiDto? dto)
        {
            if (dto is null)
                return null;

            return new PersonModel(dto.Id, dto.FirstName, dto.LastName, dto.Email);
        }

        public static IEnumerable<PersonModel>? ToPersonModels(this IEnumerable<PersonModelApiDto>? dtos)
        {
            return dtos?.Select(d => d.ToPersonModel()!);
        }

        public static PersonModelApiDto? ToApiDto(this PersonModel? dto)
        {
            if (dto is null)
                return null;

            return new PersonModelApiDto(dto.Id, dto.FirstName, dto.LastName, dto.Email);
        }

        public static IEnumerable<PersonModelApiDto>? ToApiDtos(this IEnumerable<PersonModel>? dtos)
        {
            return dtos?.Select(d => d.ToApiDto()!);
        }

        public static PersonModelDbDto? ToDbDto(this PersonModel? dto)
        {
            if (dto is null)
                return null;

            return new PersonModelDbDto
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
            };
        }

        public static IEnumerable<PersonModelDbDto>? ToDbDtos(this IEnumerable<PersonModel>? dtos)
        {
            return dtos?.Select(d => d.ToDbDto()!);
        }
    }
}
