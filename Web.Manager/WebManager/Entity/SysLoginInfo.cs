using YL.Base.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Manager.WebManager.Entity
{
    /// <summary>
    /// 系统登录类
    /// </summary>
    public class SysLoginInfo : ITransient
    {
        ISession session;
        public SysLoginInfo(IHttpContextAccessor httpContextAccessor)
        {
            session = httpContextAccessor.HttpContext.Session;
        }
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public SysManager CurAccount()
        {
            string v = session.GetString(ALLKeys.S_ADMINUSER);
            if (v == null)
                return null;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SysManager>(v);
        }
        /// <summary>
        /// 获取当前登录用户功能菜单
        /// </summary>
        /// <returns></returns>
        public List<SysMenuPage> CurMenuPages()
        {
            string v = session.GetString(ALLKeys.S_ADMINMENU);
            if (v == null)
                return null;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysMenuPage>>(v);
        }
        /// <summary>
        /// 设置当前登录用户信息
        /// </summary>
        /// <param name="manager"></param>
        public void SetCurAccount(SysManager manager)
        {
            session.SetString(ALLKeys.S_ADMINUSER, Newtonsoft.Json.JsonConvert.SerializeObject(manager));
        }
        /// <summary>
        /// 设置当前登录用户功能菜单
        /// </summary>
        /// <param name="pages"></param>
        public void SetCurMenuPages(List<SysMenuPage> pages)
        {
            session.SetString(ALLKeys.S_ADMINMENU, Newtonsoft.Json.JsonConvert.SerializeObject(pages));
        }
        /// <summary>
        /// 清空SESSION
        /// </summary>
        public void ClearSession()
        {
            session.Clear();
        }
    }
}
