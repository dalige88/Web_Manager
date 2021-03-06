﻿using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIServer.Dtos
{
    public class SubchannelDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 推广平台ID
        /// </summary>
        public long PlatformID { get; set; }
        /// <summary>
        /// 推广平台名称
        /// </summary>
        public string PlatformName { get; set; }
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
        /// 是否有效Name
        /// </summary>
        public string StatesName { get; set; }
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
        


    }
}
