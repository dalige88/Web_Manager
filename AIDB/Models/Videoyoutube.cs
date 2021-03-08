using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Videoyoutube
    {
        public long Id { get; set; }
        public string YwTitle { get; set; }
        public string ZwTitle { get; set; }
        public string Downloadurls { get; set; }
        public string Localsrc { get; set; }
        public DateTime? Downloadtime { get; set; }
        public DateTime? Posttime { get; set; }
        public int? Downloadstate { get; set; }
        public int? Poststate { get; set; }
    }
}
