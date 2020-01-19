using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fuel.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace fuel
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



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();



            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
                c.AddPolicy("AllowMethod", options => options.AllowAnyMethod());
                c.AddPolicy("AllowHeader", options => options.AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "fuelapi",
                    Description = "fuel api ",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Tendai Mungovi", Email = "tendai@rubieminnovations.xyz", Url = "http://rubieminnovations.xyz/" }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project fuelapi V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();



            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseMvc();
            app.Run(t => t.Response.WriteAsync("fuel api 404"));
        }
    }
}
