using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using wwwTools.依赖注入_Test;

namespace wwwWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env,ILoggerFactory logger)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connStr = configuration.GetSection("Data:DefaultConnection:ConnectionString").Value;

            //ILoggerFactory: 提供创建日志的接口，可以选用已经实现接口的类或自行实现此接口,下面代码使用最简单的控制台作为日志输出。
            //var log = logger.CreateLogger("Default");
            //logger.AddConsole();
            //log.LogInformation("start configure");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.Configure<IISOptions>(options => { options.ForwardClientCertificate = false; });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //测试依赖注入
            services.AddTransient<IWwwDemo_DI,WwwDemo_DI>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">IApplicationBuilder:  用于构建应用请求管道。通过IApplicationBuilder下的run方法传入管道处理方法。
        ///                                         这是最常用方法，对于一个真实环境的应用基本上都需要比如权限验证、跨域、异常处理等。
        ///                                         下面代码调用IApplicationBuilder.Run方法注册处理函数。
        ///                                         拦截每个http请求，输出Hello World</param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run((context) =>
            {
                var str = context.RequestServices.GetRequiredService<IWwwDemo_DI>().GetWww();
                return context.Response.WriteAsync(str);
            });


        }
    }
}
