using MadLearning.API;
using MadLearning.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

// var dto = JsonSerializer.Deserialize<CreateEventModelApiDto>("{ \"name\": \"Something\", \"description\": \"descc\" }");

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .Run();
