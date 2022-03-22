using System;
using Volo.Abp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MsDemo.Shared;

namespace ProductService.Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<ProductServiceHostModule>();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerOperationNameFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
           app.InitializeApplication();
        }
    }
}
