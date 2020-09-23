namespace MadLearning.API.Config
{
    public record EventDbSettings
    {
        public string EventCollectionName { get; init; }
        public string ConnectionString { get; init; }
        public string DatabaseName { get; init; }
    }
}
