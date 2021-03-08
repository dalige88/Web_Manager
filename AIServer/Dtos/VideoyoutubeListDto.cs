using System;
using System.Collections.Generic;
using System.Text;

namespace AIServer.Dtos
{
    public class VideoyoutubeListDto
    {
        public long Id { get; set; }
        public string YwTitle { get; set; }
        public string ZwTitle { get; set; }
        public string Downloadurls { get; set; }
        public string Localsrc { get; set; }
        public DateTime? Downloadtime { get; set; }
        public DateTime? Posttime { get; set; }
        public int? Downloadstate { get; set; }
        public string DownloadStateName { get; set; }
        public int? Poststate { get; set; }
        public string PostStateName { get; set; }
    }
}
