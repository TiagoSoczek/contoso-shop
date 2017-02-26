using Contoso.Shop.Api.Shared.Dtos;
using Contoso.Shop.Api.Shared.Filters;
using Contoso.Shop.Infra.Shared.Data;
using Contoso.Shop.Infra.Shared.Repositories;
using Contoso.Shop.Model.Catalog.Handlers;
using Contoso.Shop.Model.Shared.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Contoso.Shop.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x =>
            {
                x.Filters.Add(typeof(ValidatorActionFilter));
                x.Filters.Add(typeof(HandleErrorFilter));
                x.Filters.Add(typeof(DataContextTransactionFilter));
                x.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResultDto), 400));
                x.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResultDto), 500));
            })
            .AddJsonOptions(x => x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);

            services.AddDbContext<ShopDataContext>(x => 
                x.UseSqlServer(Configuration.GetConnectionString("ContosoShop"))
            );

            services.AddScoped<ProductHandlers>();
            services.AddScoped<DepartamentHandlers>();
            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ContosoShop API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
