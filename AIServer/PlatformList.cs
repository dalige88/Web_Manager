using AIDB.Models;
using AIServer.Reqs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YL.Base;
using YL.Base.Manager.Entity;

namespace AIServer
{
    public class PlatformList : AIServiceBase
    {
        public PlatformList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 根据条件查询  推广平台列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxResult<Pagination<Platforminfo>> GetList(PlatforminfoReq req)
        {
            Pagination<Platforminfo> page = new Pagination<Platforminfo>();
            var query = from b in db.Platforminfo
                        select b;
            if (!string.IsNullOrWhiteSpace(req.PlatformName))
            {
                query = query.Where(w => w.PlatformName.Contains(req.PlatformName));
            }
            if (!string.IsNullOrWhiteSpace(req.AddressURL))
            {
                query = query.Where(w => w.AddressUrl.Contains(req.AddressURL));
            }
            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.Id).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<Platforminfo>>(page);
        }

        /// <summary>
        /// 添加推广平台
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> AddPlatforminfo(PlatforminfoReq req)
        {
            Platforminfo model_1 = db.Platforminfo.Where(w => w.PlatformName == req.PlatformName || w.AddressUrl == req.AddressURL).FirstOrDefault();
            if (model_1 != null)
            {
                return new AjaxResult<Object>("推广平台已存在！");
            }

            Platforminfo model = new Platforminfo();
            model.PlatformName = req.PlatformName.Trim();
            model.AddressUrl = req.AddressURL.Trim();
            model.CreateTime = DateTime.Now;
            model.Remark = req.Remark;
            db.Platforminfo.Add(model);
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("添加成功！", 0);
            }
            return new AjaxResult<Object>("添加失败！");
        }

        /// <summary>
        /// 编辑推广平台
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> EditPlatforminfo(PlatforminfoReq req)
        {
            Platforminfo model = db.Platforminfo.Where(w => w.Id == req.ID).FirstOrDefault();
            if (model == null)
            {
                return new AjaxResult<Object>("推广平台不存在！");
            }
            model.PlatformName = req.PlatformName;
            model.AddressUrl = req.AddressURL;
            model.Remark = req.Remark;
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("保存成功！", 0);
            }
            return new AjaxResult<Object>("保存失败！");

        }

        /// <summary>
        /// 删除推广平台
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AjaxResult<Object> DelPlatforminfo(long id)
        {
            Platforminfo model = db.Platforminfo.Where(w => w.Id == id).FirstOrDefault();
            if (model == null)
            {
                return new AjaxResult<Object>("推广平台不存在！");
            }

            int sh_num = db.Subchannel.Where(w => w.PlatformId == id).Count();
            if (sh_num>0)
            {
                return new AjaxResult<Object>("莫慌，先删除平台渠道后再删除平台！");
            }

            db.Platforminfo.Remove(model);
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("删除成功！", 0);
            }
            return new AjaxResult<Object>("删除失败！");
        }

        /// <summary>
        /// 查询推广平台详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Platforminfo SelPlatforminfo(long id)
        {
            return db.Platforminfo.Where(w => w.Id == id).FirstOrDefault();
        }
    }
}
