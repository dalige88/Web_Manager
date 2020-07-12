using DB.SQLITE;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Data.SQLite;
using System.Linq;
using YL.Base;

namespace YL.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        IHttpContextAccessor HttpContext;
        public ExceptionFilter(ILoggerFactory loggerFactory, IHttpContextAccessor httpContext)
        {
            HttpContext = httpContext;
            LoggerFactory = loggerFactory;
        }

        ILoggerFactory LoggerFactory { get; }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AggregateException)
                context.Exception = (context.Exception as AggregateException)?.InnerException;
            var logger = LoggerFactory.CreateLogger(context.Exception.Source);
            logger.LogError($"{context.Exception}");
            context.Result = new JsonResult(new AjaxResult<object> { code = 500, msg = context.Exception.Message + (context.Exception.InnerException != null ? context.Exception.InnerException.Message : "") });
            context.HttpContext.Response.StatusCode = 200;
            context.ExceptionHandled = true;


            //if (context.Exception is Kernel.Exception.GucException)
            //{
            //    var ex = context.Exception as Kernel.Exception.GucException;
            //    if (ex != null)
            //    {
            //        if (ex.Code == Guc.Kernel.Exception.ExceptionCodes.Instance.NotFound)
            //            context.Result = new NotFoundResult();
            //        else if (ex.Code == Guc.Kernel.Exception.ExceptionCodes.Instance.UnAuthorize)
            //            context.Result = new StatusCodeResult(Guc.Kernel.Exception.ExceptionCodes.Instance.UnAuthorize);
            //        else if (ex.Code == Guc.Kernel.Exception.ExceptionCodes.Instance.UnAuthenticated)
            //            context.Result = new StatusCodeResult(Guc.Kernel.Exception.ExceptionCodes.Instance.UnAuthenticated);
            //        else if (!context.Filters.Any(r => r is NoWrapperAttribute))
            //            context.Result = new JsonResult(new ResponseResult(context.Exception.Message, ex.Code) { Debug = ex.Debug });

            //        if (!ex.LogContent.IsNullOrWhiteSpace())
            //            logger.LogError($"[{ex.Code}]{ex.LogContent}");
            //    }
            //}

            //var exList = context.HttpContext.RequestServices.GetServices<IOnException>();
            //exList?.Foreach(r => r.OnException(context.Exception));
            var route = context.ActionDescriptor.RouteValues;
            route.TryGetValue("controller", out string control);
            route.TryGetValue("action", out string action);
            var MapMethod = control + "/" + action;//操作方法
            SQLiteParameter ErrorType = new SQLiteParameter("ErrorType", context.GetType().ToString());
            SQLiteParameter URL = new SQLiteParameter("URL", MapMethod);
            SQLiteParameter MoreTxt = new SQLiteParameter("MoreTxt", "");

            SQLiteParameter CreateTime = new SQLiteParameter("CreateTime", DateTime.Now);
            SQLiteParameter IP = new SQLiteParameter("IP", GetIP());
            SQLiteParameter ErrorTxt = new SQLiteParameter("ErrorTxt", context.Exception.Message + context.Exception.StackTrace.ToString());
            var c = SqliteAdoSessionManager.Current;
            var vvc = c.ErrorLogsDB.CreateSQLiteCommand(@"INSERT INTO ErrorLogs (
                          ErrorType,
                          URL,
                          MoreTxt,
                          CreateTime,
                          IP,
                          ErrorTxt
                      )
                      VALUES (
                          @ErrorType,
                          @URL,
                          @MoreTxt,
                          @CreateTime,
                          @IP,
                          @ErrorTxt
                      );", new SQLiteParameter[] { ErrorType, URL, MoreTxt, CreateTime, IP, ErrorTxt });
            c.ErrorLogsDB.ExecuteNonQuery(vvc);
        }
        public string GetIP()
        {
            var ip = HttpContext.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            ////最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(ip) && IsIP(ip))
            {
                return ip;
            }
            return "127.0.0.1";
        }
        public bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

    }
}
