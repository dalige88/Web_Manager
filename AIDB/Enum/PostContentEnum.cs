using System;
using System.Collections.Generic;
using System.Text;

namespace AIDB.Enum
{
    public class PostContentEnum
    {
        /// <summary>
        /// 创建类型：0：人工创建，1：AI创建
        /// </summary>
        public enum CreateType
        {
            人工创建=0,
            AI创建=1
        }
        /// <summary>
        /// 启用状态：0：启用，1：禁用
        /// </summary>
        public enum OpenStatus
        {
            启用 = 0,
            禁用 = 1
        }
        /// <summary>
        /// 创建人类型：0：管理员，1：用户
        /// </summary>
        public enum CreateUserType
        {
            管理员 = 0,
            用户 = 1
        }
        /// <summary>
        /// 发帖类型：0、纯文本，1、纯图片，2、纯视频，3、文本+图片+视频
        /// </summary>
        public enum MsgType 
        {
            纯文本 = 0,
            纯图片 = 1,
            纯视频 = 2,
            文本图片视频 = 3
        }
    }
}
