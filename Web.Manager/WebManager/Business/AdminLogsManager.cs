using Web.Manager.WebManager.Entity;
using Web.Manager.WebManager.Models;
using YL.Base;
using YL.Base.Interface;
using DB.SQLITE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YL.Base.Manager.Entity;

namespace Web.Manager.WebManager.Business
{
    public class AdminLogsManager : ServiceBase, ILogs
    {
        public AdminLogsManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(long uid, string name, string ip, int type, string typename, string mapmethod, string logcontent)
        {
            WebSysLog log = new WebSysLog();
            log.ManagerGuid = uid.ToString();
            log.ManagerAccount = name;
            log.LogIp = ip;
            log.LogType = type;
            log.LogTime = DateTime.Now;//操作时间
            log.LogName = typename;
            log.MapMethod = mapmethod;//操作方法
            log.LogContent = logcontent;//参数说明

            //根据数据表设计，进行字符串的裁剪
            log.LogContent = log.LogContent.Length > 4000 ? log.LogContent.Substring(0, 4000) : log.LogContent;
            log.MapMethod = log.MapMethod.Length > 100 ? log.MapMethod.Substring(0, 100) : log.MapMethod;

            db.WebSysLog.Add(log);
            db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit">每页显示条数</param>
        /// <param name="offset">当前页数</param>
        /// <param name="begin_time">开始时间</param>
        /// <param name="end_time">结束时间</param>
        /// <returns></returns>
        public Pagination<WebSysLog> GetMainLog(int limit, int offset, DateTime begin_time, DateTime end_time)
        {
            begin_time = begin_time.Date;
            end_time = end_time.Date.AddDays(1);
            var query = db.WebSysLog;
            //  .Where(w => w.LogTime >= begin_time && w.LogTime < end_time)
            // .OrderByDescending(w => w.Id);
            // .FlipPage(limit, offset);
            Pagination<WebSysLog> page = new Pagination<WebSysLog>();
            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.Id).Skip(offset * limit).Take(limit).ToList();

            return page;
        }

        /// <summary>
        /// SQLITE 系统操作日志
        /// </summary>
        /// <param name="limit">每页显示条数</param>
        /// <param name="offset">当前页数</param>
        /// <param name="begin_time">开始时间</param>
        /// <param name="end_time">结束时间</param>
        /// <returns></returns>
        public Pagination<OperLogs> GetOperLogsList(int limit, int offset, DateTime begin_time, DateTime end_time)
        {
            begin_time = begin_time.Date;
            end_time = end_time.Date.AddDays(1);
            Pagination<OperLogs> page = new Pagination<OperLogs>();
            var c = SqliteAdoSessionManager.Current;
            var count = c.OperLogsDB.GetSingle("select count(1) from OperLogs");
            page.TotalCount = int.Parse(count.ToString());

            int a = 0;
            int b = 20;
            if (limit > 100)
                limit = 100;
            if (limit < 1)
                limit = 20;
            a = (offset - 1) * limit;
            b = limit;
            string sql = string.Format("select * from OperLogs order by id desc limit {0} offset {1}", b, a);
            page.dataList = c.OperLogsDB.Query2<OperLogs>(sql).ToList();

            return page;





        }


    }
}
