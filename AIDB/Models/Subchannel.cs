using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Subchannel
    {
        public long Id { get; set; }
        public long? PlatformId { get; set; }
        public string SubChannelName { get; set; }
        public string AddressUrl { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? States { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string Remark { get; set; }
        public string AnalogPacket { get; set; }
        public long? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string PyscriptVideo { get; set; }
        public string PyscriptShortEssay { get; set; }
        public string PyscriptLongEssay { get; set; }
        public string PyscriptComment { get; set; }
        public string PyscriptPic { get; set; }
    }
}
