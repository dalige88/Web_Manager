using Web.Manager.WebManager.Business;
using Web.Manager.WebManager.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Manager.Controllers
{
    public class WebHomeController : BaseController
    {
        WebSYSMenuManager menuManager;
        IWebHostEnvironment Host;

        public WebHomeController(
            WebSYSMenuManager menuManager,
            AdminUser _user,
            IWebHostEnvironment host
            ) : base(_user)
        {
            this.menuManager = menuManager;
            Host = host;
        }
        // GET: Home
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (CurAccount == null)
            {
                return RedirectToAction("Login", "WebEntrance");
            }
            ViewBag.CurAccount = CurAccount;
            ViewBag.Version = Host.ContentRootPath + "/wwwroot/" + "version.txt";
            //var menus = menuManager.GetMenus(CurAccount);
            return View("~/WebManager/Views/WebHome/Index.cshtml");
        }

        [AllowAnonymous]
        public IActionResult DashBoard()
        {
            return Content("欢迎进入管理系统");
        }

        [AllowAnonymous]
        public JsonResult LoadMenu()
        {
            var r = menuManager.LoadMenu(CurAccount);
            return Json(r);
        }
    }
}