using MadLearning.API.Application.Services;
using MongoDB.Bson;

namespace MadLearning.API.Infrastructure.Services
{
    internal sealed class IdGenerator : IIdGenerator
    {
        public string Generate()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
