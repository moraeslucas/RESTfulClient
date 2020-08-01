using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace RESTfulClient
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
            var connection = Configuration.GetConnectionString("petshopdbConnection");

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            /*Whenever a new context is requested, it will be returned from the context pool if it is available;
              Otherwise a new context will be created and returned.*/
            services.AddDbContext<Models.petshopdbContext>(options => options.UseSqlServer(connection));
            //While IClientFullAccess is injected (hence decreasing coupling), it implements ClientAccessLayer one
            services.AddScoped<DataAccess.IClientFullAccess, DataAccess.ClientDataAccess>();

            services.AddControllers();

            // Register the Swagger generator. Here you can define 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            /* Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
             * specifying the Swagger JSON endpoint. */
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            /*
             * '_ => true' inside 'policy.SetIsOriginAllowed(_ => true)' is a function used by the policy to evaluate
             *  if an origin is allowed. Since 'AllowAnyOrigin' and 'AllowCredentials' can't be used together due to CORS,
             * you can make use of this 'SetIsOriginAllowed' instead.
             *      
             *  PS: Not recommended because it's an insecure configuration and can result in cross-site request forgery.
             *      
             * 
             * 'WithHeaders' allows specific headers to be sent in a CORS request;
             * It's using default values from Microsoft site
            */
            app.UseCors(policy =>
                policy.WithOrigins("https://localhost:44358", "https://petshopmanager.azurewebsites.net")
                .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                .AllowAnyMethod()
                .AllowCredentials()
            );
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
