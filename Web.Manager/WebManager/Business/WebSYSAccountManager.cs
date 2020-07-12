using Web.Manager.WebManager.Business;
using Web.Manager.WebManager.Entity;
using Web.Manager.WebManager.Models;
using YL.Base;
using YL.Base.dtos;
using YL.Base.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YL.Base.Common;

namespace Web.Manager.WebManager.Business
{
    public class WebSYSAccountManager : ServiceBase, ILogin
    {
        ISession Session;
        IHttpContextAccessor HttpContextAccessor;
        VerifyCode verifyCode;
        SysLoginInfo SysLogin;
        public WebSYSAccountManager(IServiceProvider _serviceProvider,
            IHttpContextAccessor _httpContextAccessor,
            VerifyCode _verifyCode,
            SysLoginInfo sysLogin
            ) : base(_serviceProvider)
        {
            Session = _httpContextAccessor.HttpContext.Session;
            HttpContextAccessor = _httpContextAccessor;
            this.verifyCode = _verifyCode;
            SysLogin = sysLogin;
        }

        public CurrentUserDto GetCurrentUser()
        {
            HttpContextAccessor.HttpContext.Request.Cookies.TryGetValue(ALLKeys.C_ADMINUSER, out string value);
            if (value == null)
                return null;
            var cookieInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<AdminCookieInfo>(value);
            if (cookieInfo == null)
                return null;
            var user = GetAccountInfoByID(cookieInfo.ManagerId);
            if (user == null)
                return null;
            return new CurrentUserDto()
            {
                Account = user.ManagerName,
                Uid = user.ManagerId
            };
        }
        /// <summary>
        /// 清除登陆Token
        /// </summary>
        /// <param name="UserId"></param>
        public void ClearToken(long ManagerId)
        {
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == ManagerId);
            if (user != null)
            {
                user.CurToken = "";
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据ID获取账户
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        public WebSysManager GetAccountInfoByID(long ManagerId)
        {
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == ManagerId);
            return user;
        }

        /// <summary>
        /// 根据登陆名获取账户
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public WebSysManager GetAccountByName(string Account)
        {
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerName == Account);
            return user;
        }

        public AjaxResult<WebSysManager> DoLogin(string LoginName, string Password, string VerCode)
        {
            if (string.IsNullOrEmpty(LoginName))
                return new AjaxResult<WebSysManager>("请输入登录账号");
            if (string.IsNullOrEmpty(Password))
                return new AjaxResult<WebSysManager>("请输入登录密码");
            if (string.IsNullOrEmpty(VerCode))
                return new AjaxResult<WebSysManager>("请输入登录验证码");
            //检查验证码
            if (!verifyCode.CheckVerifyCode(VerCode))
                return new AjaxResult<WebSysManager>("验证码错误");
            var sysUser = GetAccountByName(LoginName);
            if (sysUser == null)
                return new AjaxResult<WebSysManager>("登录账号无效");

            if (sysUser.ManagerPwd != Encrypt.MD5Encrypt(Password + sysUser.ManagerScal))
                return new AjaxResult<WebSysManager>("登陆密码错误");
            if (sysUser.ManagerStatus == 0)
                return new AjaxResult<WebSysManager>("禁用");
            DateTime dtNow = DateTime.Now;

            sysUser.LastLoginTime = dtNow;
            sysUser.CurToken = Encrypt.MD5Encrypt(Guid.NewGuid().ToString());
            db.SaveChanges();
            sysUser.ManagerPwd = "";
            sysUser.ManagerScal = "";


            var cookieInfo = new AdminCookieInfo()
            {
                ManagerAccount = sysUser.ManagerName,
                LastLoginTime = dtNow,
                LoginToken = sysUser.CurToken,
                ManagerId = sysUser.ManagerId,
            };
            SysManager manager = new SysManager()
            {
                IsSupper = sysUser.IsSupper,
                ManagerId = sysUser.ManagerId,
                ManagerName = sysUser.ManagerName,
                ManagerRealname = sysUser.ManagerRealname
            };
            SysLogin.SetCurAccount(manager);

            #region 权限页面内容写入
            List<WebSysMenuPage> autoPages = GetAuthPages(manager);
            List<SysMenuPage> pages = autoPages.Select(s => new SysMenuPage()
            {
                MenuId = s.MenuId,
                PageBtnname = s.PageBtnname,
                PageId = s.PageId,
                PageName = s.PageName,
                PageUrl = s.PageUrl,
                PageViewname = s.PageViewname

            }).ToList();
            SysLogin.SetCurMenuPages(pages);
            #endregion
            HttpContextAccessor.HttpContext.Response.Cookies.Append(ALLKeys.C_ADMINUSER,
                Newtonsoft.Json.JsonConvert.SerializeObject(cookieInfo),
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(8)
                });
            return new AjaxResult<WebSysManager>(sysUser);
        }

        /// <summary>
        /// 获取账户相关权限页面
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public List<WebSysMenuPage> GetAuthPages(SysManager sysUser)
        {
            List<WebSysMenuPage> autoPages = new List<WebSysMenuPage>();
            if (sysUser.IsSupper != 1)
            {
                var roleIds = db.WebSysManagerRole.Where(m => m.ManagerId == sysUser.ManagerId).Select(m => m.RoleId).Distinct().ToList();
                List<WebSysRoleMenu> roleMenus = db.WebSysRoleMenu.Where(m => roleIds.Contains(m.RoleId)).ToList();
                List<int> pageIds = new List<int>();
                foreach (var item in roleMenus)
                {
                    if (string.IsNullOrEmpty(item.PageIds))
                        continue;
                    string[] pageidArr = item.PageIds.Split(',');
                    foreach (var pageid in pageidArr)
                    {
                        int ipageId = int.Parse(pageid);
                        if (!pageIds.Contains(ipageId))
                            pageIds.Add(int.Parse(pageid));
                    }
                }
                autoPages = db.WebSysMenuPage.Where(m => pageIds.Contains(m.PageId)).ToList();
            }
            else
            {
                autoPages = db.WebSysMenuPage.ToList();
            }
            return autoPages;
        }

    }
}
