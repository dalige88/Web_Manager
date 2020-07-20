using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AIDB.Models;
using AIServer;
using AIServer.Reqs;
using Microsoft.AspNetCore.Mvc;
using WebManager.Core.Entity;

namespace Web.Manager.Controllers
{
    public class PlatforminfoController : BaseController
    {
        #region Menu

        /// <summary>
        /// 平台管理数据管理
        /// </summary>
        PlatformList pl;


        #endregion

        #region 页面
        [MenuItemAttribute("推广平台", "平台管理")]
        public IActionResult Index()
        {
            //List<Platforminfo> list= pl.ss();
            return View();
        }

        #endregion


        #region Ajax调用
        [MenuItemAttribute("推广平台", "平台管理", "平台管理（获取）")]
        public JsonResult GetList(PlatforminfoReq req)
        {
            return Json(pl.GetList(req));
        }




        #endregion

    }
}
