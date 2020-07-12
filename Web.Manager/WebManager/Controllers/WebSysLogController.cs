using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Manager.WebManager.Business;
using Web.Manager.WebManager.Entity;
using Web.Manager.WebManager.Models;
using YL.Base;
using Microsoft.AspNetCore.Mvc;
using YL.Base.Manager.Entity;

namespace Web.Manager.Controllers
{
    public class WebSysLogController : BaseController
    {
        AdminLogsManager LogsManager;
        public WebSysLogController(AdminLogsManager logsManager, AdminUser _user):base(_user)
        {
            LogsManager = logsManager;
        }
        [MenuItem("系统日志","日志")]
        public IActionResult Index()
        {
            return View("~/WebManager/Views/WebSysLog/Index.cshtml");
        }
        [MenuItem("系统日志","日志","获取日志")]
        public JsonResult GetLogList(DateTime? begin_time, DateTime? end_time, int PageSize = 20, int PageIndex = 0)
        {
            if (begin_time == null)
            {
                begin_time = DateTime.Now;
            }
            if (end_time == null)
            {
                end_time = DateTime.Now;
            }
            PageIndex--;
            AjaxResult<Pagination<WebSysLog>> result = new AjaxResult<Pagination<WebSysLog>>();
            result.data = LogsManager.GetMainLog(PageSize, PageIndex, begin_time.Value, end_time.Value);
            return Json(result);
        }


        [MenuItem("系统日志", "操作日志")]
        public IActionResult OperationLog()
        {
            return View("~/WebManager/Views/WebOperLogs/Index.cshtml");
        }
        /// <summary>
        /// SQLITE 系统操作日志
        /// </summary>
        /// <param name="limit">每页显示条数</param>
        /// <param name="offset">当前页数</param>
        /// <param name="begin_time">开始时间</param>
        /// <param name="end_time">结束时间</param>
        /// <returns></returns>
        [MenuItem("系统日志", "后端操作日志", "获取日志")]
        public JsonResult GetOperLogsList(DateTime? begin_time, DateTime? end_time, int pageSize = 20, int pageindex = 0) 
        {
            if (begin_time == null)
            {
                begin_time = DateTime.Now;
            }
            if (end_time == null)
            {
                end_time = DateTime.Now;
            }
            AjaxResult<Pagination<OperLogs>> result = new AjaxResult<Pagination<OperLogs>>();
            result.data = LogsManager.GetOperLogsList(pageSize, pageindex, begin_time.Value, end_time.Value);
            return Json(result);
        }

    }
}