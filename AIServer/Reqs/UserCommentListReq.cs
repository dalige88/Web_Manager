using System;
using System.Collections.Generic;
using System.Text;
using YL.Base.Manager.Entity;

namespace AIServer.Reqs
{
    public class UserCommentListReq : PageModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 父级评论ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 平台渠道ID
        /// </summary>
        public long PlatformID { get; set; }
        /// <summary>
        /// 评论对象类型：1：短文，2：长文：3：视频
        /// </summary>
        public int CommentType { get; set; }
        /// <summary>
        /// 评论对象ID
        /// </summary>
        public long CommentTargetID { get; set; }
        /// <summary>
        /// 评论对象标题
        /// </summary>
        public string CommentTargetTitle { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNice { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentTime { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }
        /// <summary>
        /// 回复管理员ID
        /// </summary>
        public long ManagerID { get; set; }
        /// <summary>
        /// 回复管理员昵称
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime ReplyTime { get; set; }
        /// <summary>
        /// 标记状态：0：未标记，1：意向客户，2：恶意评论
        /// </summary>
        public int SignStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }




    }
}
