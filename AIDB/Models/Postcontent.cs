using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Postcontent
    {
        public long Id { get; set; }
        public string MsgContent { get; set; }
        public DateTime CreateTime { get; set; }
        public long? CreateManagerId { get; set; }
        public long? CreateUserId { get; set; }
        public int CreateType { get; set; }
        public int OpenStatus { get; set; }
        public int CreateUserType { get; set; }
        public int MsgType { get; set; }
        public string PlatformIds { get; set; }
        public long SubChannelId { get; set; }
        public string MsgTitle { get; set; }
        public string MsgAuthor { get; set; }
        public string HeadImg { get; set; }
        public string HeadImgServer { get; set; }
    }
}
