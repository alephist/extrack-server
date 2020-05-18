using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using ExTrackAPI.Contracts;
using ExTrackAPI.Extensions;
using ExTrackAPI.Models;
using ExTrackAPI.Repositories;
using Newtonsoft.Json;

namespace ExTrackAPI
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
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(Configuration["DataContext:ConnectionString"]);
                opt.UseSnakeCaseNamingConvention();
            });
            services.AddScoped<IWrapperRepository, WrapperRepository>();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.ConfigureCors();
            services.ConfigureAuthentication(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]));
            services
                .AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
