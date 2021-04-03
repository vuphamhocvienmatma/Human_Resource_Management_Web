using Autofac;
using Human_Resource_Management_Data.Repository;
using Human_Resource_Management_Model.MongoClass;
using Human_Resource_Management_Model.Repository.MongoRepository;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web
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
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });
          
            services.AddControllersWithViews();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

            });
            services.AddResponseCompression();

            services.AddRazorPages();

            services.AddMvcCore().AddRazorViewEngine();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(HR_EmployeeRepository).Assembly)
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces().InstancePerLifetimeScope();

           
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("vi-VN"),
                // Formatting numbers, dates, etc.
                SupportedCultures = new List<CultureInfo> { new CultureInfo("vi-VN") },
                // UI strings that we have localized.
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("vi-VN") }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseResponseCaching();


            app.UseResponseCompression();

            //app.UseSession();

            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
