using MadLearning.API.Application.Dtos;
using MadLearning.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MadLearning.API.Application.Mapping
{
    public static class PersonModelMappingExtensions
    {
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
    }
}
