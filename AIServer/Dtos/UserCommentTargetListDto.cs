using System;
using System.Collections.Generic;
using System.Text;

namespace AIServer.Dtos
{
    public class UserCommentTargetListDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
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
        public long? CommentTargetID { get; set; }
        /// <summary>
        /// 评论对象标题
        /// </summary>
        public string CommentTargetTitle { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 对应表名（例如：JrttWenZhangInfo今日头条文章）
        /// </summary>
        public string TableName { get; set; }

    }
}
