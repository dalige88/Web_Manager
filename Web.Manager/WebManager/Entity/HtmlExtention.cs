using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Manager.WebManager.Entity
{
    /// <summary>
    /// HtmlHelper 扩展
    /// </summary>
    public static class HtmlExtention
    {
        /// <summary>
        /// 静态资源加入版本号
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        //public static string GetUrlContent(string content)
        //{
        //    return GetUrlContent(content);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetUrlContent(string content)
        {
            return string.Format("{0}?v={1}", content, AppSetting.VersionNo);
        }

    }
}
