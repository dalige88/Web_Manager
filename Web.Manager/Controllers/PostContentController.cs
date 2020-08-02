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
    /// <summary>
    /// 数据包
    /// </summary>
    public class PostContentController : BaseController
    {
        #region Menu
        PostContentList pc;

        #endregion

        #region 页面

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Ajax调用

        //[MenuItemAttribute("推广平台", "平台管理", "平台管理（获取）")]
        public JsonResult Ajax_GetList(PostContentReq req)
        {
            return Json(pc.GetList(req));
        }
        //写到这里了继续写 TODU
        public JsonResult Ajax_AddPostcontent(PostContentReq req)
        {
            return Json(pc.AddPostcontent(req));
        }

        #endregion
    }
}
