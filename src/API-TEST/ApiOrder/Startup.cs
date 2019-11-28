using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using MICROSERVICES.Common.Consul.Consul;
using MICROSERVICES.Common.Consul.Mvc;
using MICROSERVICES.Common.Jaeger.Jaeger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiOrder
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IServiceId, ServiceId>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddConsul();

            /*Tracer distributed*/
            services.AddJaeger();
            services.AddOpenTracing();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IConsulClient consulClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            var serviceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
            });
        }
    }
}
