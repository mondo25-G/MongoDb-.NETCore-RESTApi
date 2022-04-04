using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentManagement.Models;
using StudentManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement
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
            #region Inject services

            //Get the all the settings of section named StudentStoreDatabaseSettings from launchSettings.json
            //and map them to class StudentStoreDatabaseSettings
            services.Configure<StudentStoreDatabaseSettings>(Configuration.GetSection(nameof(StudentStoreDatabaseSettings)));

            //Whenever an interface of IStudentStoreDatabaseSettings is required, provide an instance of StudentStoreDatabaseSettings,
            //which contains the info we have provided in appsettings.json
            services.AddSingleton<IStudentStoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<StudentStoreDatabaseSettings>>().Value);
            
            //For the mongoClient in StudentService ctor to provide the database we need to know where to read the connection string
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetValue<string>("StudentStoreDatabaseSettings:ConnectionString")));
            
            //We "Tie" our IStudentService with a concrete implementation
            services.AddScoped<IStudentService, StudentService>();

            //Afterwards we need controllers to expose these student services.
            #endregion


            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents  
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.  
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),  
            // specifying the Swagger JSON endpoint.  
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
