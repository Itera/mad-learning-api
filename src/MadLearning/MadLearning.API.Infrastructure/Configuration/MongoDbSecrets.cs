using System;

namespace MadLearning.API.Infrastructure.Configuration
{
    public record MongoDbSecretsDto
    {
        public string? Username { get; init; }
        public string? Password { get; init; }

        public static implicit operator MongoDbSecrets(MongoDbSecretsDto? dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new ArgumentException($"Can't create {nameof(MongoDbSecrets)} when {nameof(Username)} is null");
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException($"Can't create {nameof(MongoDbSecrets)} when {nameof(Password)} is null");

            return new MongoDbSecrets(dto.Username, dto.Password);
        }
    }

    public record MongoDbSecrets(string Username, string Password);
}
