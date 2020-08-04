using System;
using System.Collections.Generic;
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
    /// 渠道管理
    /// </summary>
    public class SubchannelController : BaseController
    {
        #region Menu
        SubChannelList sh;

        #endregion

        #region 页面
        [MenuItemAttribute("推广平台", "推广平台渠道管理")]
        public IActionResult Index()
        {
            return View();
        }

        [MenuItemAttribute("推广平台", "推广平台渠道管理", "添加")]
        public IActionResult AddSubchannel(string pid) 
        {
            ViewBag.pid = pid;
            return View();
        }

        [MenuItemAttribute("推广平台", "推广平台渠道管理", "编辑")]
        public IActionResult EditSubchannel(long id) 
        {
            var o = sh.SelSubchannel(id);
            return View(o);
        }

        #endregion


        #region Ajax 调用
        [MenuItemAttribute("推广平台", "推广平台渠道管理", "推广平台渠道管理（获取）")]
        public JsonResult Ajax_GetList(SubchannelReq req) 
        {
            return Json(sh.GetList(req));
        }

        [MenuItemAttribute("推广平台", "推广平台渠道管理", "添加推广平台渠道（提交）")]
        public JsonResult Ajax_AddSubchannel(SubchannelReq req) 
        {
            if (string.IsNullOrWhiteSpace(req.SubChannelName))
            {
                return Json(new AjaxResult<Object>("请输入推广平台渠道名称！"));
            }
            if (string.IsNullOrWhiteSpace(req.AddressURL))
            {
                return Json(new AjaxResult<Object>("请输入渠道地址URL！"));
            }
            if (string.IsNullOrWhiteSpace(req.AnalogPacket))
            {
                return Json(new AjaxResult<Object>("请输入模拟提交数据包！"));
            }

            return Json(sh.AddSubchannel(req));
        }

        [MenuItemAttribute("推广平台", "推广平台渠道管理", "编辑推广平台渠道（提交）")]
        public JsonResult Ajax_EditSubchannel(SubchannelReq req) 
        {
            if (req.ID < 1)
            {
                return Json(new AjaxResult<Object>("请选择您要编辑的推广平台渠道！"));
            }
            if (string.IsNullOrWhiteSpace(req.SubChannelName))
            {
                return Json(new AjaxResult<Object>("请输入推广平台渠道名称！"));
            }
            if (string.IsNullOrWhiteSpace(req.AddressURL))
            {
                return Json(new AjaxResult<Object>("请输入渠道地址URL！"));
            }
            if (string.IsNullOrWhiteSpace(req.AnalogPacket))
            {
                return Json(new AjaxResult<Object>("请输入模拟提交数据包！"));
            }
            return Json(sh.EditSubchannel(req));
        }

        [MenuItemAttribute("推广平台", "推广平台渠道管理", "删除推广平台渠道（提交）")]
        public JsonResult Ajax_DelSubchannel(long id) 
        {
            if (id < 1)
            {
                return Json(new AjaxResult<Object>("请选择您要删除的推广平台渠道！"));
            }
            return Json(sh.DelSubchannel(id));
        }

        /// <summary>
        /// 根据平台ID查询所有平台信息
        /// </summary>
        /// <returns></returns>
        public JsonResult Ajax_GetAllList(long pid)
        {
            List<Subchannel> list = sh.GetAllList(pid);

            return Json(new AjaxResult<List<Subchannel>>(list));
        }
        #endregion
    }
}
