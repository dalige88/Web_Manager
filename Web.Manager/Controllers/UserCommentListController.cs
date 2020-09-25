using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIServer;
using AIServer.Reqs;
using Microsoft.AspNetCore.Mvc;
using WebManager.Core.Entity;

namespace Web.Manager.Controllers
{
    public class UserCommentListController : BaseController
    {
        #region Menu
        UserCommentList ucl;
        #endregion

        #region 页面
        [MenuItemAttribute("发帖管理", "评论发布管理", "用户评论对象列表")]
        public IActionResult ListIndex()
        {
            return View();
        }

        #endregion

        #region Ajax调用

        [MenuItemAttribute("发帖管理", "评论发布管理", "用户评论对象列表（获取）")]
        public JsonResult Ajax_GetUserCommentList(UserCommentListReq req)
        {
            return Json(ucl.GetUserCommentList(req));
        }

        [MenuItemAttribute("发帖管理", "评论发布管理", "用户评论列表（获取）")]
        public JsonResult Ajax_GetList(UserCommentTargetListReq req)
        {
            return Json(ucl.GetList(req));
        }


        #endregion

    }
}
