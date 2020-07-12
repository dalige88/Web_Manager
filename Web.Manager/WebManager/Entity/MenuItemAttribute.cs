using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Manager.WebManager.Entity
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuItemAttribute : Attribute
    {
        /// <summary>
        /// 主模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 主名称
        /// </summary>
        public string MainName { get; set; }
        /// <summary>
        /// 子名称
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 是否主页面
        /// </summary>
        public int IsMain { get; set; }
        public string ReturnType { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 当前键
        /// </summary>
        public string ItemKey { get; set; }
        /// <summary>
        /// 当前父键
        /// </summary>
        public string ItemPKey { get; set; }
        /// <summary>
        /// 子页面
        /// </summary>
        public List<MenuItemAttribute> SubPages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modulename">模块名称</param>
        /// <param name="mainname">菜单名称</param>
        /// <param name="subname">子模块名称</param>
        public MenuItemAttribute(string modulename, string mainname, string subname = "")
        {
            this.ModuleName = modulename;
            this.MainName = mainname;
            this.SubName = subname;
            if (!string.IsNullOrEmpty(subname))
            {
                this.IsMain = 2;
            }
            else
            {
                this.IsMain = 1;
            }
            SubPages = new List<MenuItemAttribute>();
        }
    }
}
