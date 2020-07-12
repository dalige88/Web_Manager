using Web.Manager.WebManager.Business;
using Web.Manager.WebManager.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YL.Filters;

namespace Web.Manager.Controllers
{
    [AllowAnonymous]
    public class WebEntranceController : Controller
    {
        WebSYSAccountManager accountManager;
        SysLoginInfo sysLoginInfo;
        IHttpContextAccessor httpContextAccessor;
        VerifyCode verifyCode;
        public WebEntranceController(
            WebSYSAccountManager _accountManager,
            SysLoginInfo _sysLoginInfo,
            IHttpContextAccessor _httpContextAccessor,
            VerifyCode _verifyCode
            )
        {
            verifyCode = _verifyCode;
            accountManager = _accountManager;
            sysLoginInfo = _sysLoginInfo;
            httpContextAccessor = _httpContextAccessor;
        }
        #region 登录
        public ActionResult Login()
        {
            if (sysLoginInfo.CurAccount() != null)
            {
                return RedirectToAction("Index", "WebHome");
            }
            else
            {
                sysLoginInfo.ClearSession();
                httpContextAccessor.HttpContext.Response.Cookies.Delete(ALLKeys.C_ADMINUSER);
            }
            return View("~/WebManager/Views/WebEntrance/Login.cshtml");
        }


        [LogAttribute("登录", 3, "LoginName,VerCode")]
        public JsonResult DoLogin(string LoginName, string Password, string VerCode)
        {
            var result = accountManager.DoLogin(LoginName, Password, VerCode);
            return Json(result);
        }


        public async System.Threading.Tasks.Task ValidateCode()
        {
            await verifyCode.ShowVerifyCodeAsync();
        }

        #endregion

        #region 退出
        [LogAttribute("退出", 3)]
        public ActionResult Logout()
        {
            var user = sysLoginInfo.CurAccount();
            if (user != null)
            {
                accountManager.ClearToken(user.ManagerId);
            }

            sysLoginInfo.ClearSession();
            httpContextAccessor.HttpContext.Response.Cookies.Delete(ALLKeys.C_ADMINUSER);
            return RedirectToAction("Login");
        }
        #endregion
    }
}