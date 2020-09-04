using System;
using System.Collections.Generic;
using System.Text;
using YL.Base.Manager.Entity;

namespace AIServer.Reqs
{
    /// <summary>
    /// 原片 今日头条查询条件
    /// </summary>
    public class YPJrttWeiTouTiaoReq : PageModel
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Content { get; set; }

    }

    /// <summary>
    /// 已发布 今日头条查询条件
    /// </summary>
    public class JrttWeiTouTiaoReq : PageModel
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 平台ID
        /// </summary>
        public long Pid { get; set; }
        /// <summary>
        /// 发布状态：0：未发布，1：已发布
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Images { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
