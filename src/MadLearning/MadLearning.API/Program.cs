using MadLearning.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// var dto = JsonSerializer.Deserialize<CreateEventModelApiDto>("{ \"name\": \"Something\", \"description\": \"descc\" }");

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .Run();
