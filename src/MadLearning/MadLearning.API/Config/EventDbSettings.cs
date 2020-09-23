using System;

namespace MadLearning.API.Config
{
    public record EventDbSettingsDto
    {
        public string? EventCollectionName { get; init; }
        public string? ConnectionString { get; init; }
        public string? DatabaseName { get; init; }

        public static implicit operator EventDbSettings(EventDbSettingsDto? dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.EventCollectionName))
                throw new ArgumentException($"Can't create {nameof(EventDbSettings)} when {nameof(EventCollectionName)} is null");
            if (string.IsNullOrWhiteSpace(dto.ConnectionString))
                throw new ArgumentException($"Can't create {nameof(EventDbSettings)} when {nameof(ConnectionString)} is null");
            if (string.IsNullOrWhiteSpace(dto.DatabaseName))
                throw new ArgumentException($"Can't create {nameof(EventDbSettings)} when {nameof(DatabaseName)} is null");

            return new EventDbSettings(dto.EventCollectionName, dto.ConnectionString, dto.DatabaseName);
        }
    }

    public record EventDbSettings(string EventCollectionName, string ConnectionString, string DatabaseName);
}
