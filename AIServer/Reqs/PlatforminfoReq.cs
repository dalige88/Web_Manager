using System;
using System.Collections.Generic;
using System.Text;
using YL.Base.Manager.Entity;

namespace AIServer.Reqs
{
    /// <summary>
    /// 推广平台
    /// </summary>
    public class PlatforminfoReq: PageModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName { get; set; }
        /// <summary>
        /// 平台网址
        /// </summary>
        public string AddressURL { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
