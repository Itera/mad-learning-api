using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Services;
using MadLearning.API.Domain.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.HostedServices
{
    internal sealed class SeedService : IHostedService
    {
        private readonly IEventRepository eventRepository;
        private readonly IIdGenerator idGenerator;
        private readonly IHostEnvironment hostEnvironment;

        public SeedService(IEventRepository eventRepository, IIdGenerator idGenerator, IHostEnvironment hostEnvironment)
        {
            this.eventRepository = eventRepository;
            this.idGenerator = idGenerator;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!this.hostEnvironment.IsDevelopment())
                return;

            var hasAnyEvent = (await this.eventRepository.GetEvents(new Dtos.EventFilterApiDto() { Limit = 1 }, cancellationToken)).Any();
            if (hasAnyEvent)
                return;

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                ".NET 5",
                "Let's create something in .NET with C# or F# and learn what's new in .NET 5",
                DateTimeOffset.UtcNow.AddHours(6),
                null,
                new PersonModel(this.idGenerator.Generate(), "Raymond", "Selvik", "raymond.selvik@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "React fagkveld",
                "Intro to React for frontend developers",
                DateTimeOffset.UtcNow.AddDays(1),
                null,
                new PersonModel(this.idGenerator.Generate(), "Tin Anh", "Nguyen", "tin.anh.nguyen@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                ".NET microservices",
                "We will create .NET microservices using .NET 5, ASP.NET Core, Project Tye and Kubernetes",
                DateTimeOffset.UtcNow.AddDays(3),
                null,
                new PersonModel(this.idGenerator.Generate(), "Lars Erik", "Røise", "lars.erik.roise@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Java microservices",
                "Set up microservices using Java, Spring Boot and Kubernetes",
                DateTimeOffset.UtcNow.AddDays(6),
                null,
                new PersonModel(this.idGenerator.Generate(), "Klara", "Opdahl", "klara.opdahl@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Fullstack F#",
                "SAFE stack can be used to develop fullstack F# applications",
                DateTimeOffset.UtcNow.AddDays(7),
                null,
                new PersonModel(this.idGenerator.Generate(), "Øyvind", "Nedregård", "oyvind.nedregard@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Multicloud with Pulumi",
                "How to setup a multicloud environment with Pulumi IaC",
                DateTimeOffset.UtcNow.AddDays(9),
                null,
                new PersonModel(this.idGenerator.Generate(), "Martin", "Othamar", "martin.othamar@itera.no")),
                cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
