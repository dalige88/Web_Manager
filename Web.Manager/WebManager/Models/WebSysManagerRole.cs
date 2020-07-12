using System;
using System.Collections.Generic;

namespace Web.Manager.WebManager.Models
{
    public partial class WebSysManagerRole
    {
        public int AutoId { get; set; }
        public int? ManagerId { get; set; }
        public int? RoleId { get; set; }
    }
}
