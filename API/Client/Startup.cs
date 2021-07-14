using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<string> dataRole = new List<string> { "Admin", "Employee" };
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSession();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    RoleClaimType = "Role",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //ValidAudience = Configuration["Jwt:Audience"],
                    ValidAudiences = dataRole,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddScoped<Address>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<EducationRepository>();
            services.AddScoped<UniversityRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<AccountRoleRepository>();
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.WithOrigins("https://localhost:44321"));
            //});

            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

           

            //app.UseCors(options => options.AllowAnyOrigin());

            app.UseRouting();
            app.UseSession();
                
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCors(options => options.WithOrigins("https://localhost:44321"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Template}/{action=Landing}/{id?}");
            });
        }
    }
}
