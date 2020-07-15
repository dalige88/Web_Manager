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
        public string UserNameData { get; set; }
        public string Remark { get; set; }
    }
}
