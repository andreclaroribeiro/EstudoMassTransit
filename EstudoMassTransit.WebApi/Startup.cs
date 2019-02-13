using EstudoMassTransit.WebApi.Consumers;
using EstudoMassTransit.WebApi.Core;
using EstudoMassTransit.WebApi.Events;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EstudoMassTransit.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMassTransit(x =>
            {
                //Consumers
                x.AddConsumer<ContaCorrenteAddedConsumer>();
                x.AddConsumer<ContaCorrenteUpdatedConsumer>();

                //Events
                x.AddRequestClient<ContaCorrenteAddedEvent>();
                x.AddRequestClient<ContaCorrenteUpdatedEvent>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(
              cfg =>
              {
                  var host = cfg.Host("localhost", "/", h => { });

                  cfg.ReceiveEndpoint(host, typeof(ContaCorrenteAddedEvent).Name, e =>
                  {
                      e.PrefetchCount = 16;
                      e.UseMessageRetry(x => x.Interval(2, 100));
                      e.Consumer<ContaCorrenteAddedConsumer>(provider);
                  });

                  cfg.ReceiveEndpoint(host, typeof(ContaCorrenteUpdatedEvent).Name, e =>
                  {
                      e.PrefetchCount = 16;
                      e.UseMessageRetry(x => x.Interval(2, 100));
                      e.Consumer<ContaCorrenteUpdatedConsumer>(provider);
                  });
              }));

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, BusService>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}