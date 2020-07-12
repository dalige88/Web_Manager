using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YL.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CS.Base;

namespace Web.Manager.WebManager.Controllers
{
    [AllowAnonymous]
    public class ApiLogsController : Controller
    {
        IConfiguration Configuration;
        public ApiLogsController(IConfiguration config)
        {
            Configuration = config;
        }
        public IActionResult Error()
        {
            return View("~/WebManager/Views/ApiLogs/Error.cshtml");
        }
        public IActionResult Index()
        {
            return View("~/WebManager/Views/ApiLogs/Index.cshtml");
        }
        public string GetOperLogsList(DateTime? begin_time, DateTime? end_time, int pageSize = 20, int pageindex = 0)
        {
            string URLPath = Configuration["ApiDomain"];
            var v = new Dictionary<string, object>();
            v.Add("begin_time", begin_time);
            v.Add("end_time", end_time);
            v.Add("pageSize", pageSize);
            v.Add("pageindex", pageindex);
            MyHttp2 my = new MyHttp2();
            return my.HttpGet(URLPath + "WebSysLog/GetOperLogsList", v).Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
        public string GetErrorList(DateTime? begin_time, DateTime? end_time, int pageSize = 20, int pageindex = 0)
        {
            string URLPath = Configuration["ApiDomain"];
            var v = new Dictionary<string, object>();
            v.Add("begin_time", begin_time);
            v.Add("end_time", end_time);
            v.Add("pageSize", pageSize);
            v.Add("pageindex", pageindex);
            MyHttp2 my = new MyHttp2();
            return my.HttpGet(URLPath + "WebSysLog/GetErrorList", v).Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }
}