using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Usercommenttargetinfo
    {
        public long Id { get; set; }
        public long PlatformId { get; set; }
        public int CommentType { get; set; }
        public string CommentTargetId { get; set; }
        public string CommentTargetTitle { get; set; }
        public string Remark { get; set; }
        public string TableName { get; set; }
        public string SoureUrl { get; set; }
    }
}
