using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL.Base.Manager.Entity
{
    /// <summary>
    /// 分页请求返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T>
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> dataList { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
