using System;
using System.Collections.Generic;

namespace Web.Manager.WebManager.Models
{
    public partial class WebSysRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? RoleStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string RoleRemark { get; set; }
    }
}
