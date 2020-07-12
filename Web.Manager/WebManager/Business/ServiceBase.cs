using Web.Manager.WebManager.Models;
using YL.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Manager.WebManager.Business
{
    public abstract class ServiceBase : ITransient
    {
        private Lazy<web_managerContext> dbContext;
        protected web_managerContext db => dbContext.Value;
        protected IServiceProvider ServiceProvider { get; }
        public ServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            dbContext = new Lazy<web_managerContext>(() => ServiceProvider.GetService<web_managerContext>());
        }
    }
}
