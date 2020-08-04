using System;
using System.Collections.Generic;
using System.Text;

namespace AIServer.Dtos
{
    public class PostContentDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string MsgContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建管理员ID
        /// </summary>
        public long CreateManagerID { get; set; }
        /// <summary>
        /// 创建管理员名称
        /// </summary>
        public string CreateManagerName { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public long CreateUserID { get; set; }
        /// <summary>
        /// 创建用户名称
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建类型：0：人工创建，1：AI创建
        /// </summary>
        public int CreateType { get; set; }
        /// <summary>
        /// 创建类型：0：人工创建，1：AI创建
        /// </summary>
        public string CreateTypeName { get; set; }
        /// <summary>
        /// 启用状态：0：启用，1：禁用
        /// </summary>
        public int OpenStatus { get; set; }
        public string OpenStatusName { get; set; }
        /// <summary>
        /// 创建人类型：0：管理员，1：用户
        /// </summary>
        public int CreateUserType { get; set; }
        public string CreateUserTypeName { get; set; }
        /// <summary>
        /// 文章类型：0、纯文本，1、纯图片，2、纯视频，3、文本+图片+视频
        /// </summary>
        public int MsgType { get; set; }
        public string MsgTypeName { get; set; }
        /// <summary>
        /// 推广平台ID
        /// </summary>
        public long PlatformID { get; set; }
        /// <summary>
        /// 推广平台名称
        /// </summary>
        public string PlatformName { get; set; }
        /// <summary>
        /// 推广平台（渠道ID）
        /// </summary>
        public long SubChannelID { get; set; }
        /// <summary>
        /// 推广平台（渠道名称）
        /// </summary>
        public string SubChannelName { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string MsgTitle { get; set; }
        /// <summary>
        /// 文章作者
        /// </summary>
        public string MsgAuthor { get; set; }
        
    }
}
