using Web.Manager.WebManager.Business;
using Web.Manager.WebManager.Models;
using YL.Base.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Manager.WebManager.Entity
{
    public class AdminUser : ITransient
    {
        SysLoginInfo info;
        WebSYSAccountManager accountManager;
        IHttpContextAccessor httpContextAccessor;
        public AdminUser(SysLoginInfo _info, WebSYSAccountManager _accountManager, IHttpContextAccessor _httpContextAccessor)
        {
            info = _info;
            accountManager = _accountManager;
            httpContextAccessor = _httpContextAccessor;
        }
        public SysManager CurAccount
        {
            get
            {
                SysManager account = null;
                account = info.CurAccount();
                if (account != null)
                    return account;
                httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(ALLKeys.C_ADMINUSER, out string value);
                if (value == null)
                    return null;
                var cookieInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<AdminCookieInfo>(value);
                if (cookieInfo == null)
                    return null;
                var user = accountManager.GetAccountInfoByID(cookieInfo.ManagerId);
                if (user == null)
                    return null;
                if (cookieInfo.LoginToken != user.CurToken)
                    return null;
                SysManager manager = new SysManager()
                {
                    IsSupper = user.IsSupper,
                    ManagerId = user.ManagerId,
                    ManagerName = user.ManagerName,
                    ManagerRealname = user.ManagerRealname,
                };
                info.SetCurAccount(manager);
                return manager;
            }
        }

        public List<SysMenuPage> CurAuthPages
        {
            get
            {
                List<SysMenuPage> list = null;
                //list = info.CurMenuPages();
                if (list != null)
                    return list;
                if (CurAccount == null)
                    return null;

                #region 权限页面内容写入
                List<WebSysMenuPage> autoPages = accountManager.GetAuthPages(CurAccount);
                List<SysMenuPage> pages = autoPages.Select(s => new SysMenuPage()
                {
                    MenuId = s.MenuId,
                    PageBtnname = s.PageBtnname,
                    PageId = s.PageId,
                    PageName = s.PageName,
                    PageUrl = s.PageUrl,
                    PageViewname = s.PageViewname

                }).ToList();
                info.SetCurMenuPages(pages);
                #endregion
                return pages;
            }
        }
    }
}
