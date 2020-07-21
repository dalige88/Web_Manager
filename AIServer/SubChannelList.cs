using AIDB.Models;
using AIServer.Reqs;
using System;
using System.Collections.Generic;
using System.Text;
using YL.Base;
using YL.Base.Manager.Entity;
using System.Linq;

namespace AIServer
{
    public class SubChannelList : AIServiceBase
    {
        public SubChannelList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 根据条件查询  推广平台子平台渠道列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Pagination<Subchannel>> GetList(SubchannelReq req)
        {
            Pagination<Subchannel> page = new Pagination<Subchannel>();
            var query = from b in db.Subchannel
                        select b;
            if (!string.IsNullOrWhiteSpace(req.SubChannelName))
            {
                query = query.Where(w => w.SubChannelName.Contains(req.SubChannelName));
            }
            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.Id).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<Subchannel>>(page);
        }

        /// <summary>
        /// 添加  推广平台子平台渠道
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> AddSubchannel(SubchannelReq req) 
        {
            Subchannel model_1 = db.Subchannel.Where(w => w.SubChannelName == req.SubChannelName).FirstOrDefault();
            if (model_1 != null)
            {
                return new AjaxResult<Object>("推广子平台渠道已存在！");
            }

            Subchannel model = new Subchannel();
            model.PlatformId = req.PlatformID;
            model.SubChannelName = req.SubChannelName;
            model.AddressUrl = req.AddressURL;
            model.CreateTime = DateTime.Now;
            model.States = 0;
            model.UserNameData = req.UserNameData;
            model.Remark = req.Remark;

            db.Add(model);
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("添加成功！", 0);
            }
            return new AjaxResult<Object>("添加失败！");
        }




    }
}
