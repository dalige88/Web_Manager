using YL.Base.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace YL.Base
{
    public abstract class ServiceBase<T> : ITransient
    {
        private Lazy<T> dbContext;
        protected T db => dbContext.Value;
        protected IServiceProvider _ServiceProvider { get; }
        public ServiceBase(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
            dbContext = new Lazy<T>(() => _ServiceProvider.GetService<T>());
        }
        protected virtual void OnCreateProperties()
        {
            object controller = this;
            //foreach (PropertyInfo declaredProperty in controller.GetType().GetTypeInfo().DeclaredProperties)
            //{
            //    if (declaredProperty.CanWrite)
            //    {
            //        declaredProperty.GetSetMethod(true).Invoke(controller, new object[1]
            //        {
            //            ActivatorUtilities.GetServiceOrCreateInstance(_ServiceProvider, declaredProperty.PropertyType)
            //        });
            //    }
            //}
            foreach (var declaredProperty in controller.GetType().GetTypeInfo().DeclaredFields)
            {
                declaredProperty.SetValue(controller,

                        ActivatorUtilities.GetServiceOrCreateInstance(_ServiceProvider, declaredProperty.FieldType)

                );
            }
        }
    }
}
