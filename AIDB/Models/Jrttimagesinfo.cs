using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Jrttimagesinfo
    {
        public ulong Id { get; set; }
        public long? PlatforminfoId { get; set; }
        public string Url { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string WebUrl { get; set; }
    }
}
