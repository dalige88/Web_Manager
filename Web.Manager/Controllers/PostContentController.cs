using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIDB.Models;
using AIServer;
using AIServer.Reqs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebManager.Core.Entity;
using YL.Base;
using YL.Base.dtos;

namespace Web.Manager.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class PostContentController : BaseController
    {
        #region Menu
        PostContentList pc;
        PlatformList pl;

        #endregion

        #region 页面

        /// <summary>
        /// 文章管理列表页面
        /// </summary>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "添加")]
        public IActionResult AddPostContent()
        {
            List<Platforminfo> list= pl.GetAllList();
            return View(list);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "编辑")]
        public IActionResult EditPostContent(long id)
        {
            List<Platforminfo> list = pl.GetAllList();
            ViewBag.pl_list = list;

            var o = pc.SelPostcontent(id);
            return View(o);
        }

        #endregion

        #region Ajax调用
        /// <summary>
        /// 文章管理（获取）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "文章管理（获取）")]
        public JsonResult Ajax_GetList(PostContentReq req)
        {
            return Json(pc.GetList(req));
        }

        /// <summary>
        /// 添加文章（提交）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "添加文章（提交）")]
        public JsonResult Ajax_AddPostcontent(PostContentReq req)
        {
            if (string.IsNullOrWhiteSpace(req.MsgTitle))
            {
                return Json(new AjaxResult<Object>("请输入标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgContent))
            {
                return Json(new AjaxResult<Object>("请输入内容！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgAuthor))
            {
                return Json(new AjaxResult<Object>("请输入作者！"));
            }
            if (req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯文本 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯图片 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯视频 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.文本图片视频)
            {
                return Json(new AjaxResult<Object>("文章类型错误！"));
            }
            if (req.PlatformID<1)
            {
                return Json(new AjaxResult<Object>("推广平台信息错误！"));
            }
            if(req.SubChannelID<1)
            {
                return Json(new AjaxResult<Object>("推广渠道信息错误！"));
            }
            req.CreateManagerID = CurAccount.ManagerId;//当前管理员ID
            return Json(pc.AddPostcontent(req));
        }

        /// <summary>
        /// 编辑文章（提交）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "编辑文章（提交）")]
        public JsonResult Ajax_EditPostcontent(PostContentReq req)
        {
            if (req.ID<1)
            {
                return Json(new AjaxResult<Object>("请选择您要编辑的文章！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgTitle))
            {
                return Json(new AjaxResult<Object>("请输入标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgContent))
            {
                return Json(new AjaxResult<Object>("请输入内容！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgAuthor))
            {
                return Json(new AjaxResult<Object>("请输入作者！"));
            }
            if (req.OpenStatus!=(int)AIDB.Enum.PostContentEnum.OpenStatus.启用&& req.OpenStatus != (int)AIDB.Enum.PostContentEnum.OpenStatus.禁用)
            {
                return Json(new AjaxResult<Object>("启用状态错误！"));
            }
            if (req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯文本 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯图片 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯视频 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.文本图片视频)
            {
                return Json(new AjaxResult<Object>("文章类型错误！"));
            }

            return Json(pc.EditPostcontent(req));
        }

        /// <summary>
        /// 删除文章（提交）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "删除文章（提交）")]
        public JsonResult Ajax_DelPostcontent(long id)
        {
            if (id < 1)
            {
                return Json(new AjaxResult<Object>("请选择您要删除的文章！"));
            }
            return Json(pc.DelPostcontent(id));
        }

        /// <summary>
        /// 根据平台ID查询所有平台信息
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
