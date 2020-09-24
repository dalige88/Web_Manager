using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Usercommentlistinfo
    {
        public long Id { get; set; }
        public long UserCommentTargetInfoId { get; set; }
        public long? ParentId { get; set; }
        public string UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserNice { get; set; }
        public string CommentContent { get; set; }
        public DateTime? CommentTime { get; set; }
        public string ReplyContent { get; set; }
        public long? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public DateTime? ReplyTime { get; set; }
        public int? SignStatus { get; set; }
        public string Remark { get; set; }
    }
}
