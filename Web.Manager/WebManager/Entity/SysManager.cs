using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Manager.WebManager.Entity
{
    public class SysManager
    {

        // 摘要: 
        //     是否超级用户
        public int? IsSupper { get; set; }
        //
        // 摘要: 
        //     用户ID
        public int ManagerId { get; set; }
        //
        // 摘要: 
        //     用户名
        public string ManagerName { get; set; }
        //
        // 摘要: 
        //     用户真实名
        public string ManagerRealname { get; set; }
    }
}
