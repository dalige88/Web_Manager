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
    /// <summary>
    /// 平台管理
    /// </summary>
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

        [MenuItemAttribute("推广平台", "平台管理","添加")]
        public IActionResult AddPlatforminfo() 
        {
            return View();
        }

        [MenuItemAttribute("推广平台", "平台管理", "编辑")]
        public IActionResult EditPlatforminfo(long id)
        {
            var o = pl.SelPlatforminfo(id);
            return View(o);
        }

        #endregion


        #region Ajax调用
        [MenuItemAttribute("推广平台", "平台管理", "平台管理（获取）")]
        public JsonResult GetList(PlatforminfoReq req)
        {
            return Json(pl.GetList(req));
        }

        [MenuItemAttribute("推广平台", "平台管理", "添加平台（提交）")]
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

        [MenuItemAttribute("推广平台", "平台管理", "编辑平台（提交）")]
        public JsonResult Ajax_EditPlatforminfo(PlatforminfoReq req) 
        {
            if (req.ID<1)
            {
                return Json(new AjaxResult<Object>("请选择您要编辑的平台！"));
            }
            if (string.IsNullOrWhiteSpace(req.PlatformName))
            {
                return Json(new AjaxResult<Object>("请输入平台名称！"));
            }
            if (string.IsNullOrWhiteSpace(req.AddressURL))
            {
                return Json(new AjaxResult<Object>("请输入平台地址！"));
            }

            return Json(pl.EditPlatforminfo(req));
        }

        [MenuItemAttribute("推广平台", "平台管理", "删除平台（提交）")]
        public JsonResult Ajax_DelPlatforminfo(long id) 
        {
            if (id < 1)
            {
                return Json(new AjaxResult<Object>("请选择您要删除的平台！"));
            }

            return Json(pl.DelPlatforminfo(id));
        }

        /// <summary>
        /// 查询所有平台信息
        /// </summary>
        /// <returns></returns>
        public JsonResult Ajax_GetAllList()
        {
            List<Platforminfo> list = pl.GetAllList();

            return Json(new AjaxResult<List<Platforminfo>>(list));
        }

        #endregion

    }
}
