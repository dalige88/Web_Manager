using System;
using System.Collections.Generic;
using System.Text;

namespace YL.Base.dtos
{
    public class APIUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 用户登录账号
        /// </summary>
        public string useraccount { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 最后登录日期 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string updateTime { get; set; }
        /// <summary>
        /// 失效时间，未使用 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string lastTime { get; set; }
        /// <summary>
        /// 微信OPENID
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unionid { get; set; }
        
    }
}
