using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SQLite;
using DB.SQLITE;
using DB.SQLITE.TABLE;
using YL.Base.Interface;

namespace YL.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        IServiceProvider ServiceProvider;
        //ILogs Logs;
        ILogin Login;
        IHttpContextAccessor HttpContext;
        ILoggerFactory LoggerFactory { get; }
        public LogFilter(IServiceProvider serviceProvider, ILogin login, IHttpContextAccessor httpContext, ILoggerFactory loggerFactory)
        {
            HttpContext = httpContext;
            // Logs = logs;
            Login = login;
            LoggerFactory = loggerFactory;
            ServiceProvider = serviceProvider;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string ParamContent = Newtonsoft.Json.JsonConvert.SerializeObject(context.ActionArguments);
            var userInfo = Login.GetCurrentUser();
            if (userInfo == null)
                userInfo = new Base.dtos.CurrentUserDto();
            var ip = GetIP();

            //记录系统日志
            string req_url = HttpContext.HttpContext.Request.Path.ToString();
            AddData(req_url, userInfo, ParamContent);

            var md = context.ActionDescriptor.EndpointMetadata.Where(a => a.GetType().Equals(typeof(LogAttribute))).FirstOrDefault();
            // ControllerActionDescriptor v = context.ActionDescriptor as ControllerActionDescriptor;
            if (md == null)
                return;
            
            var logattr = md as LogAttribute;
            var route = context.ActionDescriptor.RouteValues;
            route.TryGetValue("controller", out string control);
            route.TryGetValue("action", out string action);
            var MapMethod = control + "/" + action;//操作方法

            try
            {
                ILogs Logs = ServiceProvider.GetService<ILogs>();
                Logs.Add(userInfo.Uid, userInfo.Account, ip, logattr._type, logattr._LogTypeName, MapMethod, ParamContent);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger(nameof(LogFilter));
                logger.LogError(ex, "LogFilter");
            }
            //base.OnActionExecuting(context);
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
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



        /// <summary>
        ///  SQLITE （后台所有访问页面操作日志） 插入数据
        /// </summary>
        public void AddData(string req_url,object userInfo,object param)
        {
           //创建插入列表数据
            SQLiteParameter URL = new SQLiteParameter("URL", req_url);
            SQLiteParameter UserInfo = new SQLiteParameter("UserInfo", Newtonsoft.Json.JsonConvert.SerializeObject(userInfo));
            SQLiteParameter CreateTime = new SQLiteParameter("CreateTime", DateTime.Now);
            SQLiteParameter IP = new SQLiteParameter("IP", GetIP());
            SQLiteParameter Param = new SQLiteParameter("Param", param.ToString());


            //执行插入
            var c = SqliteAdoSessionManager.Current;
            var vvc = c.OperLogsDB.CreateSQLiteCommand(@"INSERT INTO OperLogs (
                             URL,
                             Param,
                             UserInfo,
                             CreateTime,
                             IP
                         )
                         VALUES (
                             @URL,
                             @Param,
                             @UserInfo,
                             @CreateTime,
                             @IP
                         );", new SQLiteParameter[] { URL, Param, UserInfo, CreateTime, IP });
            c.OperLogsDB.ExecuteNonQuery(vvc);
        }




    }
}
