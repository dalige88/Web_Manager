using System;
using System.Collections.Generic;

namespace Web.Manager.WebManager.Models
{
    public partial class WebSysLog
    {
        public int Id { get; set; }
        public string ManagerGuid { get; set; }
        public int? LogType { get; set; }
        public string LogContent { get; set; }
        public DateTime? LogTime { get; set; }
        public string LogName { get; set; }
        public string ManagerAccount { get; set; }
        public string MapMethod { get; set; }
        public string LogIp { get; set; }
    }

    /// <summary>
    /// 系统操作日志
    /// </summary>
    public partial class OperLogs 
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public string Param { get; set; }
        public string UserInfo { get; set; }
        public DateTime CreateTime { get; set; }
        public string IP { get; set; }

    }
}
