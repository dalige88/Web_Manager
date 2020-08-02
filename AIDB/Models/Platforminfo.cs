using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Platforminfo
    {
        public long Id { get; set; }
        public string PlatformName { get; set; }
        public string AddressUrl { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Remark { get; set; }
    }
}
