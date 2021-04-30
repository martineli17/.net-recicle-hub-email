using Dominio.Contratos;
using HubEmail.Core;
using HubEmail.Core.BackgroundServices;
using HubEmail.Core.Contratos;
using HubEmail.Core.Observables.Contratos;
using HubEmail.Core.Observables.Handlers;
using Mensageria.Contratos;
using Mensageria.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repositorio;

namespace HubEmail
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsOptions", x =>
                                                x.AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowCredentials()
                                                .WithOrigins("http://localhost:3000")));
            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "HubEmail", Version = "v1" }));
            services.AddTransient(x => new Contexto(Configuration.GetConnectionString("Hub"), "Hub"));
            services.AddTransient<IRabbit, Rabbit>(x => new Rabbit(Configuration.GetConnectionString("RabbitMq")));
            services.AddTransient<IHubRepositorio, HubRepositorio>();
            services.AddTransient<IEmailEnviadosColetaHub, EmailEnviadosColetaHub>();
            services.AddSingleton<IEmailEnviadosColetaObservable, EmailEnviadosColetaObservable>();
            services.AddHostedService<EmailEnviadosColetaBackgroundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HubEmail v1"));
            }

            app.UseCors("CorsOptions");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<EmailEnviadosColetaHub>("/hubs/email/enviados/coleta");
            });
        }
    }
}
