using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Manager.WebManager.Entity
{
    /// <summary>
    /// 静态资源版本
    /// </summary>
    public class AppSetting
    {
        //版本号
        internal static string VersionNo;
        /// <summary>
        /// 生成新的版本
        /// </summary>
        public static void Start()
        {
            //加载版本号
            StartVersionNo();
        }

        #region 版本号相关
        private static void StartVersionNo()
        {
            if (string.IsNullOrEmpty(VersionNo))
                VersionNo = CreateVersionNo();
        }

        private static string ChangeVersionNo()
        {
            VersionNo = CreateVersionNo();
            return VersionNo;
        }

        private static string CreateVersionNo()
        {
            return Guid.NewGuid().ToString("N");
        }

        private static string GetVersionNo()
        {
            if (string.IsNullOrEmpty(VersionNo))
            {
                VersionNo = CreateVersionNo();
            }
            return VersionNo;
        }
        #endregion
    }
}