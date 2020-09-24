using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebManager.Core.Entity;

namespace Web.Manager.Controllers
{
    public class UserCommentListController : Controller
    {
        #region Menu

        #endregion

        #region 页面
        [MenuItemAttribute("发帖管理", "评论发布管理")]
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Ajax调用

   



        #endregion

    }
}
