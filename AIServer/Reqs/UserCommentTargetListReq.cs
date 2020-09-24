using System;
using System.Collections.Generic;
using System.Text;
using YL.Base.Manager.Entity;

namespace AIServer.Reqs
{
    public class UserCommentTargetListReq : PageModel
    {
        /// <summary>
        /// 平台渠道ID
        /// </summary>
        public long PlatformID { get; set; }
        /// <summary>
        /// 评论对象类型：1：短文，2：长文：3：视频
        /// </summary>
        public int CommentType { get; set; }
        /// <summary>
        /// 评论对象标题
        /// </summary>
        public string CommentTargetTitle { get; set; }


    }
}
