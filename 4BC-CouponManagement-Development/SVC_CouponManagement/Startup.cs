using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CouponManagementDBEntity.Repository;
using CouponManagementDBEntity.Models;
using CouponManagement.Helper;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using CouponManagement.Extensions;

namespace CouponManagement
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<ICouponRepository,CouponRepository>();
            //services.AddTransient<ICouponManagementHelper, CouponManagementHelper>();
            //services.AddMvc();
            services.AddMvcCore(
             config =>
             {

                 config.Filters.Add(typeof(CustomExceptionFilter));

             }
           );
            services.AddTransient<ICouponRepository, CouponRepository>();
            services.AddTransient<ICouponManagementHelper, CouponManagementHelper>();
            services.AddTransient<CouponManagementContext>();
            services.AddControllers()
      .AddControllersAsServices();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "CouponManagement", Version = "v1" ,
                    Description = "Provides Coupon Functionalities ,\r\n Repository Url:https://github.com/SravaniDurga123/4BC-CouponManagement.git"});


                // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API COUPON V1");
                c.RoutePrefix = String.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
