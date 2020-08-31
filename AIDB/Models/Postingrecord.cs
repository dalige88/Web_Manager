using System;
using System.Collections.Generic;

namespace AIDB.Models
{
    public partial class Postingrecord
    {
        public ulong Id { get; set; }
        public long PlatformId { get; set; }
        public long SubChannelId { get; set; }
        public long PostContentId { get; set; }
        public string MsgTitle { get; set; }
        public string PostData { get; set; }
        public DateTime PostTime { get; set; }
        public int PostType { get; set; }
        public long ManagerId { get; set; }
        public string ReturnData { get; set; }
        public uint? Success { get; set; }
    }
}
