using System;

namespace MadLearning.API.Domain.Entities
{
    public record PersonModel
    {
        public PersonModel(string id, string firstName, string lastName, string email)
        {
            this.Id = id ?? throw new InvalidOperationException("Person can only be created from valid DB dto");
            this.FirstName = firstName ?? throw new InvalidOperationException("Person can only be created from valid DB dto");
            this.LastName = lastName ?? throw new InvalidOperationException("Person can only be created from valid DB dto");
            this.Email = email ?? throw new InvalidOperationException("Person can only be created from valid DB dto");
        }

        public PersonModel(string firstName, string lastName, string email)
        {
            this.Id = string.Empty;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public string Id { get; init; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
