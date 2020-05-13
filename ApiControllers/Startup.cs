using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ApiControllers.Models;
using Microsoft.Net.Http.Headers;

namespace ApiControllers
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository, MemoryRepository>();

            services.AddMvc()
                //gives mvc ability to send xml back in its response
                .AddXmlDataContractSerializerFormatters()

                //adding shorthand mapping for xml (json mapping is there by default)
                .AddMvcOptions(opts =>
                {
                    opts.FormatterMappings.SetMediaTypeMappingForFormat("xml",
                        new MediaTypeHeaderValue("application/xml"));
                    opts.RespectBrowserAcceptHeader = true;
                    
                    //sends 406-Not Acceptable to client if a formatter not found
                    opts.ReturnHttpNotAcceptable = true;
                });
        }
                
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
