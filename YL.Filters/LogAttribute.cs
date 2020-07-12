using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace YL.Filters
{
    public class LogAttribute : Attribute
    {
        public string _parameterNameList = "";
        public string _LogTypeName = "";
        public int _type;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogTypeName">名称</param>
        /// <param name="tp">操作类型(0新增 1修改 2删除 3登录)</param>
        /// <param name="Param">未用</param>
        public LogAttribute(string LogTypeName, int tp, string Param = "")
        {
            _LogTypeName = LogTypeName;
            _parameterNameList = Param;
            _type = tp;
        }
    }
}
