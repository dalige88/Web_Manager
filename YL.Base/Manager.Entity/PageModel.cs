using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL.Base.Manager.Entity
{
    /// <summary>
    /// 分页请求基础类
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortFiled { get; set; }
        /// <summary>
        /// 排序方式：desc,asc
        /// </summary>
        public string SortType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PageModel()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        public PageModel(int pageindex, int pagesize, int totalcount)
        {
            this.PageIndex = pageindex;
            this.PageSize = pagesize;
        }
    }
}
