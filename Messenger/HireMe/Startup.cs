using AutoMapper;
using HireMe.Data;
using HireMe.Data.Repository;
using HireMe.Data.Repository.Interfaces;
using HireMe.Middlewares.Extensions;
using HireMe.Models;
using HireMe.Services;
using HireMe.Services.Interfaces;
using HireMe.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Reflection;
using HireMe.Utility;
using Microsoft.Extensions.Hosting;
using System;

namespace HireMe
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


            // Connection string
          
            services.AddDbContext<BaseDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<FeaturesDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            // Identity configuration
            services.Configure<IdentityOptions>(optinons =>
            {
                optinons.SignIn.RequireConfirmedEmail = false;
                optinons.Password.RequiredLength = 3;
                optinons.Password.RequireLowercase = false;
                optinons.Password.RequireNonAlphanumeric = false;
                optinons.Password.RequireLowercase = false;
                optinons.Password.RequireUppercase = false;
                optinons.Password.RequiredUniqueChars = 0;
            });


            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<BaseDbContext>();

            /*   services.AddMvc(options =>
               {
                   options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

               });*/
            services.AddAutoMapper();

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(DbBaseRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(DbFeaturesRepository<>));

            // App Services
            services.AddTransient<IAccountsService, AccountsService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IBaseService, BaseService>();


            // Email Sender
            //services.Configure<AuthMessageSenderOptions>(Configuration);

            // Singleton
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRouting(option =>
            {
                option.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
                option.LowercaseUrls = true;
            });



            // .NET Core 3.1
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Email Options
            services.Configure<DataProtectionTokenProviderOptions>(o =>o.TokenLifespan = TimeSpan.FromHours(3));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

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

            //  app.UseStatusCodePagesWithRedirects("/Client/Errors?code={0}");
            app.UseSeedRoles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"uploads")),
                RequestPath = new PathString("/uploads")
            });

          //  app.UseCookiePolicy();

            // .NET Core 3.0
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Base}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}
