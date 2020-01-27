using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
//añadidos
using Microsoft.EntityFrameworkCore;
using ApiSwaggers.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;

namespace ApiSwaggers
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS
            services.AddCors(o => o
                .AddPolicy("CorsPolicy", builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                })
            );
            //CORS  [EnableCors ("CorsPolicy")]

            // Add framework services.
            var connection = @"Server=10.151.158.43;Database=entrega_segura;Trusted_Connection=False;User ID=sa;Password=Password_01";
            services.AddDbContext<ApiContext.Api>(options => options.UseSqlServer(connection));
            //services.AddDbContext<ApiContext.Api>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Odata
            services.AddOData();
            //Odata
            //SWAGGER
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddMvc();
            services.AddSwaggerGen(swagger =>
                {
                    var contact = new Contact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
                    swagger.SwaggerDoc(
                        SwaggerConfiguration.DocNameV1, 
                        new Info
                        {
                            Title = SwaggerConfiguration.DocInfoTitle,
                            Version = SwaggerConfiguration.DocInfoVersion,
                            Description = SwaggerConfiguration.DocInfoDescription,
                            Contact = contact
                        }
                    );
                }
            );
            //SWAGGER            

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //CORS
            app.UseCors("CorsPolicy");
            //CORS

            //SWAGGER
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //SWAGGER

            //Odata            
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<Cliente>("Clientes").EntityType.Count().Filter().OrderBy().Expand().Select();
            builder.EntitySet<Correo>("Correos").EntityType.Count().Filter().OrderBy().Expand().Select();

            app.UseMvc(routeBuilder => {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Expand().Select().OrderBy().Filter();
                routeBuilder.MapODataServiceRoute("ODataRoute", "api", builder.GetEdmModel());
            });
            //Odata

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            
            app.UseMvc();
        }
    }
}