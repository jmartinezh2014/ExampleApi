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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;




namespace ExampleApi
{
    public class Startup
    {
        
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

          services.AddDbContext<TodoContext>(options =>
         options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ConexionSQL-2;Trusted_Connection=True;MultipleActiveResultSets=true"));

         

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                builder => {
                    builder.WithOrigins(Configuration.GetValue<string>("Url_Client"), Configuration.GetValue<string>("Url_Api_1"))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });



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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
