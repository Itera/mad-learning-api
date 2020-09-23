using MadLearning.API.Config;
using MadLearning.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace MadLearning.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EventDbSettingsDto>(
                    this.Configuration.GetSection(nameof(EventDbSettings)));

            services.AddSingleton<EventDbSettings>(static sp =>
                    sp.GetRequiredService<IOptions<EventDbSettingsDto>>().Value);

            services.AddSingleton<IEventRepository, EventRepository>();

            services.AddControllers();
            services.AddSwaggerGen(static c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MadLearning.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(static c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MadLearning.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(static endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
