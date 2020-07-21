using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Manager;
using DB.SQLITE;
using DB.SQLITE.TABLE;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Manager.WebManager.Business;
using Web.Manager.WebManager.Entity;
using Web.Manager.WebManager.Models;
using YL.Base.Interface;
using YL.Filters;
using WebManager.Core;
using AIDB.Models;

namespace Web.Manager
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
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            var c = SqliteAdoSessionManager.Current;
            var i2 = c.ErrorLogsDB.Query2<sqlite_master>("select * from sqlite_master where type = 'table' and name = 'ErrorLogs'");
            if (i2.Count == 0)
                c.ErrorLogsDB.ExecuteSql(SQLTableSentence.SQL_ErrorLogs);
            i2 = c.OperLogsDB.Query2<sqlite_master>("select * from sqlite_master where type = 'table' and name = 'OperLogs'");
            if (i2.Count == 0)
                c.OperLogsDB.ExecuteSql(SQLTableSentence.SQL_OperLogs);

            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddDbContext<web_managerContext>(opt => opt.UseMySql(Configuration.GetConnectionString("web_manager")));
            services.AddDbContext<ai_platformContext>(opt => opt.UseMySql(Configuration.GetConnectionString("ai_platform")));
            web_managerContext a = new web_managerContext();
            if(a.Database.GetPendingMigrations().Any())
            {
                a.Database.Migrate();
            }
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddControllersWithViews(r =>
            {
                r.Filters.Add<AuthorizeAttribute>(0);
                r.Filters.Add<LogFilter>(1);
            }).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            services.AddDI();

            services.AddTransient(typeof(ILogs), typeof(AdminLogsManager));
            services.AddTransient(typeof(ILogin), typeof(WebSYSAccountManager));
            services.LoadPluginServices();

            AppSetting.Start();
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
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=WebHome}/{action=Index}/{id?}");
            });
        }
    }
}
