﻿using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Services;
using MadLearning.API.Domain.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
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

        public static DateTimeOffset FloorMinutes(DateTimeOffset dt)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, dt.Offset);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!this.hostEnvironment.IsDevelopment())
                return;

            var hasAnyEvent = (await this.eventRepository.GetEvents(new Dtos.EventFilterApiDto() { Limit = 1 }, cancellationToken)).Any();
            if (hasAnyEvent)
                return;

            var imageUrl = "https://itera-cdn.azureedge.net/contentassets/df1f34b7803045fa95ffd4529826f2b2/kristian-redi-2.jpg?quality=60&Cache=Always&width=1148&mode=crop&scale=both";
            var imageAlt = "Picture of programmers";
            var location = "Jernlageret";
            var eventType = EventType.CodingEvent;

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                ".NET 5",
                "Let's create something in .NET with C# or F# and learn what's new in .NET 5",
                FloorMinutes(DateTimeOffset.UtcNow.AddHours(6)),
                FloorMinutes(DateTimeOffset.UtcNow.AddHours(7)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Raymond", "Selvik", "raymond.selvik@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "React fagkveld",
                "Intro to React for frontend developers",
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(1)),
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(1).AddHours(2)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Tin Anh", "Nguyen", "tin.anh.nguyen@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Vue fagkveld",
                "Intro to Vue for frontend developers",
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(1).AddHours(2)),
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(1).AddHours(3)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Tin Anh", "Nguyen", "tin.anh.nguyen@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Microsoft Orleans",
                "Intro to stateful processing with virtual actors",
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(1).AddHours(4)),
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(1).AddHours(5)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Martin", "Othamar", "martin.othamar@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                ".NET microservices",
                "We will create .NET microservices using .NET 5, ASP.NET Core, Project Tye and Kubernetes",
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(3)),
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(3).AddHours(2)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Lars Erik", "Røise", "lars.erik.roise@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Java microservices",
                "Set up microservices using Java, Spring Boot and Kubernetes",
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(6)),
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(6).AddHours(2)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Klara", "Opdahl", "klara.opdahl@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Fullstack F#",
                "SAFE stack can be used to develop fullstack F# applications",
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(7)),
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(7).AddHours(2)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Øyvind", "Nedregård", "oyvind.nedregard@itera.no")),
                cancellationToken);

            await this.eventRepository.CreateEvent(
                EventModel.Create(
                "Multicloud with Pulumi",
                "How to setup a multicloud environment with Pulumi IaC",
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(9)),
                FloorMinutes(DateTimeOffset.UtcNow.AddDays(9).AddHours(2)),
                imageUrl,
                imageAlt,
                location,
                eventType,
                new PersonModel(this.idGenerator.Generate(), "Martin", "Othamar", "martin.othamar@itera.no")),
                cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
