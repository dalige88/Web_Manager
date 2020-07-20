using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebManager.Core
{
    public static class LoadPlugin
    {
        public static IServiceCollection LoadPluginServices(this IServiceCollection services)
        {
            return services;
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            var assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Plugins/DemoPlugin1/DemoPlugin1.dll");
            var assemblyView = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Plugins/DemoPlugin1/DemoPlugin1.Views.dll");
            var mvcBuilders = services.AddMvcCore();
            var viewAssemblyPart = new CompiledRazorAssemblyPart(assemblyView);

            var controllerAssemblyPart = new AssemblyPart(assembly);

            mvcBuilders.ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts.Add(controllerAssemblyPart);
                apm.ApplicationParts.Add(viewAssemblyPart);
            });
            return services;
        }
    }
}
