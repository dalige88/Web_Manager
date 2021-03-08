using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIServer;
using AIServer.Reqs;
using Microsoft.AspNetCore.Mvc;
using WebManager.Core.Entity;
using YL.Base;

namespace Web.Manager.Controllers
{
    /// <summary>
    /// YouTuBe视频列表
    /// </summary>
    public class VideoYouTuBeController : BaseController
    {
        #region Menu
        VideoYouTuBeList vytb;
        #endregion

        #region 页面
        [MenuItemAttribute("发帖管理", "YouTuBe视频管理")]
        public IActionResult Index()
        {
            return View();
        }

        [MenuItemAttribute("发帖管理", "YouTuBe视频管理", "编辑视频")]
        public IActionResult EditYouTuBe(long id)
        {
            var o = vytb.SelVideoyoutube(id);
            return View(o);
        }

        [MenuItemAttribute("发帖管理", "YouTuBe视频管理", "添加视频")]
        public IActionResult AddYouTuBe()
        {
            return View();
        }


        #endregion

        #region Ajax调用

        [MenuItemAttribute("发帖管理", "YouTuBe视频管理", "视频列表（获取）")]
        public JsonResult Ajax_GetList(VideoYouTuBeListReq req)
        {
            return Json(vytb.GetList(req));
        }

        /// <summary>
        /// 添加视频（提交）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "YouTuBe视频管理", "添加视频（提交）")]
        public JsonResult Ajax_AddVideoyoutube(VideoYouTuBeReq req)
        {
            if (string.IsNullOrWhiteSpace(req.yw_title))
            {
                return Json(new AjaxResult<Object>("请输入英文标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.zw_title))
            {
                return Json(new AjaxResult<Object>("请输入中文标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.downloadurls))
            {
                return Json(new AjaxResult<Object>("请输入下载地址！"));
            }
            
            return Json(vytb.AddVideoyoutube(req));
        }

        [MenuItemAttribute("发帖管理", "YouTuBe视频管理", "编辑视频（提交）")]
        public JsonResult Ajax_EditVideoyoutube(VideoYouTuBeReq req)
        {
            if (req.ID<1)
            {
                return Json(new AjaxResult<Object>("请选择您要操作的视频！"));
            }
            if (string.IsNullOrWhiteSpace(req.yw_title))
            {
                return Json(new AjaxResult<Object>("请输入英文标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.zw_title))
            {
                return Json(new AjaxResult<Object>("请输入中文标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.downloadurls))
            {
                return Json(new AjaxResult<Object>("请输入下载地址！"));
            }

            return Json(vytb.EditVideoyoutube(req));
        }

        [MenuItemAttribute("发帖管理", "YouTuBe视频管理", "删除视频（提交）")]
        public JsonResult Ajax_DelVideoyoutube(long id)
        {
            if (id < 1)
            {
                return Json(new AjaxResult<Object>("请选择您要删除的视频！"));
            }
            return Json(vytb.DelVideoyoutube(id));
        }


        #endregion

    }
}
