using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Manager.WebManager.Entity
{
    public class AdminCookieInfo
    {
        public long ManagerId { get; set; }
        public string ManagerAccount { get; set; }
        public string LoginToken { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
