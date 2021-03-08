using System;
using System.Collections.Generic;
using System.Text;
using YL.Base.Manager.Entity;

namespace AIServer.Reqs
{
    public class VideoYouTuBeListReq : PageModel
    {
        /// <summary>
        /// 英文标题
        /// </summary>
        public string yw_title { get; set; }
        /// <summary>
        /// 中文标题
        /// </summary>
        public string zw_title { get; set; }
    }

    /// <summary>
    /// YouTuBe视频 实体
    /// </summary>
    public class VideoYouTuBeReq 
    {
        public long ID { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
        public string yw_title { get; set; }
        /// <summary>
        /// 中文标题
        /// </summary>
        public string zw_title { get; set; }
        /// <summary>
        /// 下载网址url
        /// </summary>
        public string downloadurls { get; set; }
        /// <summary>
        /// 本地存储地址
        /// </summary>
        public string localsrc { get; set; }
        /// <summary>
        /// 下载时间
        /// </summary>
        public DateTime downloadtime { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime posttime { get; set; }
        /// <summary>
        /// 下载状态：0：未下载，1：已下载，-1：下载失败
        /// </summary>
        public int downloadstate { get; set; }
        /// <summary>
        /// 发布状态：0：未发布，1：已发布，-1：发布失败
        /// </summary>
        public int poststate { get; set; }
    }
}
