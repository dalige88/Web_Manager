﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AIDB.Models;
using AIServer;
using AIServer.Reqs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Manager.WebManager.Entity;
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
        AdminUser user;
        #endregion

        #region 页面
        [MenuItemAttribute("推广平台", "推广平台渠道管理")]
        public IActionResult Index()
        {
            return View();
        }

        [MenuItemAttribute("推广平台", "推广平台渠道管理", "添加")]
        public IActionResult AddSubchannel() 
        {
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
            if (string.IsNullOrWhiteSpace(req.PYScript_Video)&& 
                string.IsNullOrWhiteSpace(req.PYScript_ShortEssay)&& 
                string.IsNullOrWhiteSpace(req.PYScript_LongEssay)&& 
                string.IsNullOrWhiteSpace(req.PYScript_Comment)&&
                string.IsNullOrWhiteSpace(req.PYScript_PIC))
            {
                return Json(new AjaxResult<Object>("必须填写至少一个PY执行脚本程序！"));
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
            req.ManagerID = user.CurAccount.ManagerId;
            req.ManagerName = user.CurAccount.ManagerName;
            return Json(sh.AddSubchannel(req));
        }

        [MenuItemAttribute("推广平台", "推广平台渠道管理", "编辑推广平台渠道（提交）")]
        public JsonResult Ajax_EditSubchannel(SubchannelReq req) 
        {
            if (string.IsNullOrWhiteSpace(req.PYScript_Video) &&
                string.IsNullOrWhiteSpace(req.PYScript_ShortEssay) &&
                string.IsNullOrWhiteSpace(req.PYScript_LongEssay) &&
                string.IsNullOrWhiteSpace(req.PYScript_Comment)&&
                string.IsNullOrWhiteSpace(req.PYScript_PIC))
            {
                return Json(new AjaxResult<Object>("必须填写至少一个PY执行脚本程序！"));
            }
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
        /// 查询所有渠道信息
        /// </summary>
        /// <returns></returns>
        public JsonResult Ajax_GetAllList(int types=0)
        {
            List<Subchannel> list = sh.GetAllList(types);

            return Json(new AjaxResult<List<Subchannel>>(list));
        }
        #endregion
    }
}
