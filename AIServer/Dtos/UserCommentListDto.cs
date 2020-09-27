﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AIServer.Dtos
{
    public class UserCommentListDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 父级评论ID
        /// </summary>
        public long? ParentID { get; set; }
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
        public string CommentTime { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }
        /// <summary>
        /// 回复管理员ID
        /// </summary>
        public long? ManagerID { get; set; }
        /// <summary>
        /// 回复管理员昵称
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public string ReplyTime { get; set; }
        /// <summary>
        /// 标记状态：0：未标记，1：意向客户，2：恶意评论
        /// </summary>
        public int? SignStatus { get; set; }
        /// <summary>
        /// 标记状态名称
        /// </summary>
        public string SignStatusName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 评论对象表ID
        /// </summary>
        public string UserCommentTargetInfoID { get; set; }
        /// <summary>
        /// 评论ID（dongtai_id）
        /// </summary>
        public string dongtai_id { get; set; }
        /// <summary>
        /// 评论对象ID（item_id）
        /// </summary>
        public string CommentTargetID { get; set; }
        /// <summary>
        /// 评论对象标题
        /// </summary>
        public string CommentTargetTitle { get; set; }


    }
}
