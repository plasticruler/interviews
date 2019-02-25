using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Realmdigital_Interview.Configuration;
using Realmdigital_Interview.MapperProfiles;
using Realmdigital_Interview.Repository;


namespace Realmdigital_Interview
{
    public class Startup
    {
        ILoggerFactory _loggerFactory;
        public Startup()
        {

        }

        public IConfiguration ConfigurationData { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //we use singleton here because the file won't change and it's therefore ok to keep instance alive
            //if it were transient you would recreate instance and by implication read the file back in
            var mappingConfig  = new MapperConfiguration(mc=>{
                mc.AddProfile(new DomainProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddLogging();            
            services.AddSingleton<IConfigurationData, ConfigurationData>();
            services.AddSingleton<IProductRepository, ProductFileRepository>(); //I won't bother with dropping in the webservice. in production we would demand the vendor rewrite it.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}