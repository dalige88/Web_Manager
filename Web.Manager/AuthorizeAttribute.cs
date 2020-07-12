using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Manager.WebManager.Entity;
using YL.Base;

namespace Web.Manager
{
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        AdminUser User;
        IUrlHelperFactory UrlHelperFactory;
        public AuthorizeAttribute(
            IUrlHelperFactory urlHelperFactory, AdminUser _user)
        {
            User = _user;
            UrlHelperFactory = urlHelperFactory;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isDefined = context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
            if (isDefined)
                return;
            bool isAjax = IsAjaxPage(context);
            var CurAccount = User.CurAccount;
            if (CurAccount == null)
            {
                DoMainLogout(context, isAjax);
                return;
            }
            //RecordLog(context, CurAccount);
            bool result = AuthorizeCheck(context, CurAccount, isAjax);
            if (result == false)
                return;

            base.OnActionExecuting(context);
        }

        protected bool IsAjaxPage(ActionExecutingContext filterContext)
        {
            string ActionName = filterContext.ActionDescriptor.RouteValues["action"];
            bool isAjax = false;
            var methods = filterContext.Controller.GetType().GetMethods().ToList();
            foreach (var method in methods)
            {
                if (method.ReturnType != typeof(JsonResult))
                    continue;
                if (method.Name.ToLower() == ActionName.ToLower())
                {
                    isAjax = true;
                    break;
                }
            }
            return isAjax;
        }

        protected void DoMainLogout(ActionExecutingContext filterContext, bool IsAjax)
        {
            filterContext.HttpContext.Request.Cookies.TryGetValue(ALLKeys.C_ADMINUSER, out string value);
            if (value != null)
                filterContext.HttpContext.Response.Cookies.Delete(ALLKeys.C_ADMINUSER);
            if (IsAjax)
            {
                JsonResult jr = new JsonResult(new AjaxResult<Object>("账户权限失效", -1));
                filterContext.Result = jr;
                return;
            }
            else
            {
                //var Url = UrlHelperFactory.GetUrlHelper(filterContext);
                //filterContext.HttpContext.Response.
                //     WriteAsync(string.Format("<script>top.window.location.href='{0}';</script>", Url.Action("Logout", "WebEntrance"))).Wait();
                // filterContext.HttpContext.Abort();
                filterContext.Result = new RedirectResult("/WebEntrance/Logout"); ;
                return;
            }
        }

        private bool AuthorizeCheck(ActionExecutingContext filterContext, SysManager user, bool isAjax)
        {
            if (user.IsSupper == 1)
                return true;
            var CurAuthPages = User.CurAuthPages;

            var route = filterContext.ActionDescriptor.RouteValues;
            route.TryGetValue("controller", out string ControllerName);
            route.TryGetValue("action", out string ActionName);

            string thisUrl = string.Format("/{0}/{1}", ControllerName, ActionName).ToLower();
            var autoPage = CurAuthPages.FirstOrDefault(m => m.PageUrl.ToLower() == thisUrl);
            if (autoPage == null)
            {
                if (isAjax)
                {
                    JsonResult jr = new JsonResult(new AjaxResult<Object>("你无访问权限" + thisUrl));
                    filterContext.Result = jr;
                }
                else
                {
                    filterContext.HttpContext.Response.WriteAsync("你无访问权限" + thisUrl).Wait();
                    // filterContext.HttpContext.Abort();
                }
                return false;
            }
            return true;
        }
    }
}
