using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Manager.WebManager.Entity
{
    /// <summary>
    /// 功能菜单
    /// </summary>
    public class SysMenuPage
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int? MenuId { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        public string PageUrl { get; set; }
        /// <summary>
        /// 按钮名
        /// </summary>
        public string PageBtnname { get; set; }
        /// <summary>
        /// 链接名
        /// </summary>
        public string PageViewname { get; set; }
        /// <summary>
        /// 页面ID
        /// </summary>
        public int PageId { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        public string PageName { get; set; }
        
    }
}
