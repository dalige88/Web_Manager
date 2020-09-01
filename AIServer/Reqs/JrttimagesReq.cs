using System;
using System.Collections.Generic;
using System.Text;
using YL.Base.Manager.Entity;

namespace AIServer.Reqs
{
    public class JrttimagesReq : PageModel
    {
        public long ID { get; set; }
        /// <summary>
        /// 平台ID
        /// </summary>
        public long PlatforminfoID { get; set; }
        /// <summary>
        /// 本地服务器文件  物理路径
        /// </summary>
        public string Url { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        /// <summary>
        /// 头条图片地址
        /// </summary>
        public string WebUrl { get; set; }
        /// <summary>
        /// PY执行脚本路径
        /// </summary>
        public string PYScript { get; set; }
        /// <summary>
        /// 图片类型
        /// </summary>
        public string MimeType { get; set; }
    }
}
