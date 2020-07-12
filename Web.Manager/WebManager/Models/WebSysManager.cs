using System;
using System.Collections.Generic;

namespace Web.Manager.WebManager.Models
{
    public partial class WebSysManager
    {
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPwd { get; set; }
        public string ManagerScal { get; set; }
        public string ManagerRealname { get; set; }
        public string ManagerTel { get; set; }
        public string ManagerEmail { get; set; }
        public int? ManagerIsdel { get; set; }
        public int? ManagerStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? IsSupper { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string CurToken { get; set; }
    }
}
