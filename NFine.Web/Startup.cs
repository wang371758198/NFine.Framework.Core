using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NFine.Data;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NFine.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
           // System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc();
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });

            //添加mvc自定义寻址Razor页面地址
            mvcBuilder.AddRazorOptions(options =>
            {
                options.ViewLocationExpanders.Add(new NamespaceViewLocationExpander());
            });

            services.AddAuthentication("IdentityCookieAuthenScheme").AddCookie("IdentityCookieAuthenScheme",options =>
            {
                options.LoginPath = "/Login/Index";
                options.Cookie.Name = "AuthCookie";
            });
            services.AddDbContextPool<NFineDbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(Configuration.GetSection("connectionStrings:NFineDbContext").Value, options => options.UseRowNumberForPaging());
#if DEBUG
                optionsAction.ConfigureWarnings(warningsConfigurationBuilderAction => warningsConfigurationBuilderAction.Throw(RelationalEventId.QueryClientEvaluationWarning));
#endif
            });
            services.AddScoped<IRepositoryBase, RepositoryBase>();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseMiddleware<NFine.Code.Middleware.ExceptionHandlerMiddleware>();
            }

            ///注册全局先上问关联的HttpContext
            System.Web.HttpContext.Configure(app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),env);

            //注册全局Configuration对象
            NFine.Code.ConfigurationManager.Configure(Configuration);

            app.UseSession();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "ExampleManage",
                    template: "ExampleManage/{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "ReportManage",
                    template: "ReportManage/{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "SystemManage",
                  template: "SystemManage/{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                 name: "SystemSecurity",
                 template: "SystemSecurity/{controller}/{action=Index}/{id?}");
            });
        }
    }
}
