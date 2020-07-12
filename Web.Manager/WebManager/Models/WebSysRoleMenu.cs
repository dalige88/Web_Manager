using System;
using System.Collections.Generic;

namespace Web.Manager.WebManager.Models
{
    public partial class WebSysRoleMenu
    {
        public int AutoId { get; set; }
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
        public string PageIds { get; set; }
    }
}
