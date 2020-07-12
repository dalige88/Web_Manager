using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Manager.WebManager.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Manager.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            OnCreateProperties(context);
        }
        protected virtual void OnCreateProperties(ActionExecutingContext context)
        {
            object controller = context.Controller;
            //foreach (PropertyInfo declaredProperty in controller.GetType().GetTypeInfo().DeclaredProperties)
            //{
            //    if (declaredProperty.CanWrite)
            //    {
            //        declaredProperty.GetSetMethod(true).Invoke(controller, new object[1]
            //        {
            //            ActivatorUtilities.GetServiceOrCreateInstance(context.HttpContext.RequestServices, declaredProperty.PropertyType)
            //        });
            //    }
            //}
            foreach (var declaredProperty in controller.GetType().GetTypeInfo().DeclaredFields)
            {
                declaredProperty.SetValue(controller,

                        ActivatorUtilities.GetServiceOrCreateInstance(context.HttpContext.RequestServices, declaredProperty.FieldType)

                );
            }
            foreach (var declaredProperty in controller.GetType().BaseType.GetTypeInfo().DeclaredFields)
            {
                declaredProperty.SetValue(controller,

                        ActivatorUtilities.GetServiceOrCreateInstance(context.HttpContext.RequestServices, declaredProperty.FieldType)

                );
            }
        }

        AdminUser user;
        //public BaseController(AdminUser _user) {
        //    user = _user;
        //}
        protected SysManager CurAccount
        {
            get
            {
                return user.CurAccount;
            }
        }

        protected List<SysMenuPage> CurAuthPages
        {
            get
            {
                return user.CurAuthPages;
            }
        }


    }
}