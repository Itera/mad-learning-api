using System;
using System.ComponentModel.DataAnnotations;

namespace MadLearning.API.Application.Dtos
{
    public sealed class PersonModelDbDto
    {
        public string? Id { get; init; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }
    }
}
