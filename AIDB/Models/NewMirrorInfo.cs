using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class NewMirrorInfo
    {
        public long Id { get; set; }
        public string YwTitle { get; set; }
        public string ZwTitle { get; set; }
        public string Urls { get; set; }
        public int Status { get; set; }
    }
}
