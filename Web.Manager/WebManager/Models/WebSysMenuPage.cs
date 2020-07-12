using System;
using System.Collections.Generic;

namespace Web.Manager.WebManager.Models
{
    public partial class WebSysMenuPage
    {
        public int PageId { get; set; }
        public int? MenuId { get; set; }
        public int? MainStatus { get; set; }
        public string PageName { get; set; }
        public int? PageStatus { get; set; }
        public string PageViewname { get; set; }
        public string PageBtnname { get; set; }
        public int? PageType { get; set; }
        public string PageUrl { get; set; }
        public string PageParamters { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
