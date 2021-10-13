using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Features;
using ZennoLab.Api.Model;
using ZennoLab.Api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ZennoLab.Api
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZennoLab.Api", Version = "v1" });
            });

            string connection = Configuration.GetConnectionString("ZennoLab");
            services.AddDbContext<SqlContext>(options => options.UseSqlServer(connection));

            var dataSetUploadOptions = new DataSetUploadOptions();
            Configuration.GetSection("DataSetUploadOptions").Bind(dataSetUploadOptions);
            services.AddSingleton<DataSetUploadOptions>(dataSetUploadOptions);


            services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                     .AllowAnyHeader()));

            services.AddSingleton(s => new MapperConfiguration(cfg =>           
                cfg.AddMaps(this.GetType().Assembly)
            ).CreateMapper());

            //services.Configure<FormOptions>(
            //    options =>
            //    {
            //        options.MultipartBodyLengthLimit = 80000000;
            //        options.ValueLengthLimit = int.MaxValue;
            //        options.MultipartHeadersLengthLimit = int.MaxValue;
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZennoLab.Api v1"));
            }
            app.UseCors();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
