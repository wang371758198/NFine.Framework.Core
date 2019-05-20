using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using NFine.Code;
using Quartz;
using CrystalQuartz.AspNetCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using NFine.Data;

namespace NFine.Scheduler
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region 注入repositorybase类
            Assembly asm = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("NFine.Repository"));
            var typesToRegister = asm.GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace) && type.IsPublic).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(RepositoryBase<>));

            foreach (var type in typesToRegister)
            {
                if (type.IsClass)
                {
                    services.AddScoped(type.GetInterface("I" + type.Name), type);
                }
            }
            #endregion

            #region 注入app类
            var nfineApplication = Microsoft.Extensions.DependencyModel.DependencyContext.Default.CompileLibraries.FirstOrDefault(_ => _.Name.Equals("NFine.Application"));
            var application = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(nfineApplication.Name));
            var apps = application.GetTypes().Where(_ => _.IsClass && _.IsPublic).Where(type => !String.IsNullOrEmpty(type.Namespace));
            foreach (var type in apps)
            {
                services.AddScoped(type, type);
            }
            #endregion

            services.AddTransient<Jobs.HelloJob>();
            services.AddSingleton<QuartzStartup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IApplicationLifetime lifetime, IHostingEnvironment env)
        {
            var quartz = app.ApplicationServices.GetRequiredService<QuartzStartup>();
            lifetime.ApplicationStarted.Register(quartz.Start);
            lifetime.ApplicationStopped.Register(quartz.Stop);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseCrystalQuartz(() => quartz.Scheduler);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        
    }
}
