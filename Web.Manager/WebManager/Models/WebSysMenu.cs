using System;
using System.Collections.Generic;

namespace Web.Manager.WebManager.Models
{
    public partial class WebSysMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int? MenuPid { get; set; }
        public string MenuIcon { get; set; }
        public string IndexCode { get; set; }
        public string MenuUrl { get; set; }
        public int? MenuStatus { get; set; }
        public string MenuItempages { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? MenuSort { get; set; }
    }
}
