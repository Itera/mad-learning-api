using MadLearning.API.Models;
using System;

namespace MadLearning.API.Dtos
{
    public record GetEventModelApiDto(string Id, string Name, string Description)
    {
        public static GetEventModelApiDto FromModel(EventModel? model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            return new GetEventModelApiDto(model.Id, model.Name, model.Description);
        }
    }
}
