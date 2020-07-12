using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YL.Base.Interface;

namespace Web.Manager
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class Dependcy
    {
        public static void AddDI(this IServiceCollection services)
        {
            var ss = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name;
            var allLib = DependencyContext.Default.RuntimeLibraries.OrderBy(r => r.Name).ToList();
            Dictionary<string, HashSet<string>> allDependcies = new Dictionary<string, HashSet<string>>();
            foreach (var v in allLib)
            {
                allDependcies.Add(v.Name, new HashSet<string>());
                loadAllDependcy(allLib, v.Name, new HashSet<string>(), v, allDependcies);
            }
            ////////////////////////////////////////////////////////
            var name = typeof(ITransient).Assembly.GetName().Name;
            var assemblies = allDependcies.Where(r => r.Value.Contains(name)).Select(r => Assembly.Load(r.Key)).ToList();
            foreach (var assemblie in assemblies)
            {
                var configTypes = assemblie
                               .DefinedTypes
                               .Where(t =>
                                 !t.IsAbstract && t.BaseType != null && t.IsClass && t.ImplementedInterfaces != null
                                 && t.ImplementedInterfaces.Contains(typeof(ITransient))).ToList();
                foreach (var t in configTypes)
                {
                    //if (t.ImplementedInterfaces.Contains(typeof(IWorkFlow)))
                    //{
                    //    services.AddTransient(typeof(IWorkFlow), t);
                    //}
                    //else if (t.ImplementedInterfaces.Contains(typeof(ISnsapiBaseOAuthNotify)))
                    //{
                    //    services.AddTransient(typeof(ISnsapiBaseOAuthNotify), t);
                    //}
                    //else if (t.ImplementedInterfaces.Contains(typeof(IBussniessUser)))
                    //{
                    //    services.AddTransient(typeof(IBussniessUser), t);
                    //}
                    //else if (t.ImplementedInterfaces.Contains(typeof(IBussniessDetails)))
                    //{
                    //    services.AddTransient(typeof(IBussniessDetails), t);
                    //}
                    //else if (t.ImplementedInterfaces.Contains(typeof(IMessage)))
                    //{
                    //    services.AddTransient(typeof(IMessage), t);
                    //}
                    //else if (t.ImplementedInterfaces.Contains(typeof(IBussniess)))
                    //{
                    //    services.AddTransient(typeof(IBussniess), t);
                    //}
                    //else if (t.ImplementedInterfaces.Contains(typeof(IOrderPay)))
                    //{
                    //    services.AddTransient(typeof(IOrderPay), t);
                    //}
                    //else
                    {
                        services.AddTransient(t);
                    }
                }
            }
            //////////////////////////////////////////////////
            //name = typeof(IWorkFlow).Assembly.GetName().Name;
            //assemblies = allDependcies.Where(r => r.Value.Contains(name)).Select(r => Assembly.Load(r.Key)).ToList();
            //foreach (var assemblie in assemblies)
            //{
            //    var configTypes = assemblie
            //                   .DefinedTypes
            //                   .Where(t =>
            //                     !t.IsAbstract && t.BaseType != null && t.IsClass && t.ImplementedInterfaces != null
            //                     && t.ImplementedInterfaces.Contains(typeof(IWorkFlow))).ToList();
            //    foreach (var t in configTypes)
            //        services.AddTransient(typeof(IWorkFlow), t);
            //}
        }
        private static void loadAllDependcy(IEnumerable<RuntimeLibrary> allLibs, string key, HashSet<string> handled, RuntimeLibrary current, Dictionary<string, HashSet<string>> allDependcies)
        {
            if (current.Dependencies == null || !current.Dependencies.Any())
                return;
            if (handled.Contains(current.Name))
                return;
            handled.Add(current.Name);

            foreach (var item in current.Dependencies)
            {
                allDependcies[key].Add(item.Name);
                var next = allLibs.FirstOrDefault(r => r.Name == item.Name);
                if (next == null || next.Dependencies == null || !next.Dependencies.Any())
                    continue;
                loadAllDependcy(allLibs, key, handled, next, allDependcies);
            }
        }

    }
}
