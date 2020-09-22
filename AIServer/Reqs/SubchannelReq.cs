using System;
using System.Collections.Generic;
using System.Text;
using YL.Base.Manager.Entity;

namespace AIServer.Reqs
{
    public class SubchannelReq : PageModel
    {
        /// <summary>
        /// PY视频发布脚本（视频）
        /// </summary>
        public string PYScript_Video { get; set; }
        /// <summary>
        /// PY短文发布脚本（微头条）
        /// </summary>
        public string PYScript_ShortEssay { get; set; }
        /// <summary>
        /// PY长文发布脚本（文章）
        /// </summary>
        public string PYScript_LongEssay { get; set; }
        /// <summary>
        /// PY评论发布脚本（评论）
        /// </summary>
        public string PYScript_Comment { get; set; }
        /// <summary>
        /// PY图库发布脚本（图库）
        /// </summary>
        public string PYScript_PIC { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 发布管理员ID
        /// </summary>
        public long ManagerID { get; set; }
        /// <summary>
        /// 发布管理员名称
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 子渠道名称
        /// </summary>
        public string SubChannelName { get; set; }
        /// <summary>
        /// 渠道地址URL（模拟提交数据包）
        /// </summary>
        public string AddressURL { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否有效：0：有效，1：无效
        /// </summary>
        public int States { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 模拟提交数据包
        /// </summary>
        public string AnalogPacket { get; set; }
    }
}
