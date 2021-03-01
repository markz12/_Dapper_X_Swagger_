using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; //for dbcontext
using _Dapper_X_Swagger_.Services;

namespace _Dapper_X_Swagger_
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
            services.AddControllers();

            #region SwaggerService
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Dapper X Swagger API",
                    Description = "Using dapper & Swagger Technology for API Development",
                    Version = "v1"
                });

                var filename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, filename);
                options.IncludeXmlComments(filePath);
            });
            #endregion

            #region Dapper Configuration
                                                                    //Install Microsoft.EntityFrameworkCore.SqlServer to call the UseSqlServer Functionality.
            services.AddDbContext<DataContext.AppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default"))); // add this to read the dbcontext and assigned connection string
            services.AddScoped<IGlobalRepository, GlobalRepository>();  //declare Interface Dapper and Dapper Repository to read this classes during startup(Mark)
            #endregion

            #region This will be used to add asp views
            services.AddControllersWithViews(); //this service is used to read the mvc view in the asp core api version
            #endregion
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

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper x Swagger API");
                options.RoutePrefix = "";
            });
            #endregion
        }
    }
}
