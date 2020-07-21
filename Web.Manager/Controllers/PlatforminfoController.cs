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
using YL.Base;

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
            return View();
        }

        [MenuItemAttribute("推广平台", "平台管理","添加推广平台")]
        public IActionResult AddPlatforminfo() 
        {
            return View();
        }

        #endregion


        #region Ajax调用
        [MenuItemAttribute("推广平台", "平台管理", "平台管理（获取）")]
        public JsonResult GetList(PlatforminfoReq req)
        {
            return Json(pl.GetList(req));
        }

        public JsonResult Ajax_AddPlatforminfo(PlatforminfoReq req) 
        {
            if (string.IsNullOrWhiteSpace(req.PlatformName))
            {
                return Json(new AjaxResult<Object>("请输入平台名称！"));
            }
            if (string.IsNullOrWhiteSpace(req.AddressURL))
            {
                return Json(new AjaxResult<Object>("请输入平台地址！"));
            }
            
            return Json(pl.AddPlatforminfo(req));
        }


        #endregion

    }
}
