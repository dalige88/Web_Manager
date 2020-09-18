using System;
using System.Collections.Generic;
using System.Text;

namespace AIServer.Dtos
{
    /// <summary>
    /// 原片  今日头条微头条
    /// </summary>
    public class YPJrttWeiTouTiaoDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 微头条内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 图片列表
        /// </summary>
        public string Images { get; set; }
        /// <summary>
        /// 平台ID
        /// </summary>
        public long? PlatformID { get; set; }
        /// <summary>
        /// 发布状态：0：未发布，1：头条平台已发布
        /// </summary>
        public int? status { get; set; }
        /// <summary>
        /// 源链接
        /// </summary>
        public string SourceLink { get; set; }
    }

    /// <summary>
    /// 已发布 今日头条微头条
    /// </summary>
    public class JrttWeiTouTiaoDto 
    {
        public long ID { get; set; }

        public string Content { get; set; }
        public string Thread_Id { get; set; }
        public string Ugc_U13_Cut_Image_List { get; set; }
        public string Publish_Time { get; set; }

    }
}
