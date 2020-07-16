using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIDB.Models;
using AIServer;
using Microsoft.AspNetCore.Mvc;
using WebManager.Core.Entity;

namespace Web.Manager.Controllers
{
    public class PlatforminfoController : BaseController
    {
        /// <summary>
        /// 平台管理数据管理
        /// </summary>
        PlatformList pl;

        [MenuItemAttribute("测试", "平台管理")]
        public IActionResult Index()
        {
            List<Platforminfo> list= pl.ss();
            return View();
        }



    }
}
