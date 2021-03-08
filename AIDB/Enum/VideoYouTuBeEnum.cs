using System;
using System.Collections.Generic;
using System.Text;

namespace AIDB.Enum
{
    /// <summary>
    /// video_youtube视频列表
    /// </summary>
    public class VideoYouTuBeEnum
    {
        /// <summary>
        /// 下载状态：0：未下载，1：已下载，-1：下载失败
        /// </summary>
        public enum DownloadState
        {
            未下载 = 0,
            已下载 = 1,
            下载失败 = -1,
        }

        /// <summary>
        /// 发布状态：0：未发布，1：已发布，-1：发布失败
        /// </summary>
        public enum PostState
        {
            未发布 = 0,
            已发布 = 1,
            发布失败 = -1,
        }
    }
}
