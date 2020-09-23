namespace MadLearning.API.Models
{
    public sealed class PersonModel
    {
        public string? Id { get; init; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }
    }
}
