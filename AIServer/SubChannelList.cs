using AIDB.Models;
using AIServer.Reqs;
using System;
using System.Collections.Generic;
using System.Text;
using YL.Base;
using YL.Base.Manager.Entity;
using System.Linq;
using AIServer.Dtos;

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
        public AjaxResult<Pagination<SubchannelDto>> GetList(SubchannelReq req)
        {
            Pagination<SubchannelDto> page = new Pagination<SubchannelDto>();
            var query = from b in db.Subchannel
                        join c in db.Platforminfo on b.PlatformId equals c.Id
                        select new SubchannelDto
                        {
                            ID = b.Id,
                            PlatformID = (long)b.PlatformId,
                            SubChannelName = b.SubChannelName,
                            AddressURL = b.AddressUrl,
                            CreateTime = Convert.ToDateTime(b.CreateTime),
                            States = Convert.ToInt32(b.States),
                            UserName = b.UserName,
                            UserPwd = b.UserPwd,
                            Remark = b.Remark,
                            PlatformName = c.PlatformName,
                            StatesName = b.States == 0 ? "有效" : b.States == 1 ? "无效" : "",

                        };
            if (!string.IsNullOrWhiteSpace(req.SubChannelName))
            {
                query = query.Where(w => w.SubChannelName.Contains(req.SubChannelName));
            }
            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ID).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<SubchannelDto>>(page);
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
            model.UserName = req.UserName;
            model.UserPwd = req.UserPwd;
            model.Remark = req.Remark;
            model.AnalogPacket = req.AnalogPacket;

            db.Add(model);
            db.SaveChanges();
            return new AjaxResult<Object>("添加成功！", 0);
        }

        /// <summary>
        /// 编辑推广平台子平台
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> EditSubchannel(SubchannelReq req)
        {
            Subchannel model = db.Subchannel.Where(w => w.Id == req.ID).FirstOrDefault();
            if (model == null)
            {
                return new AjaxResult<Object>("推广子平台渠道不存在！");
            }

            model.SubChannelName = req.SubChannelName;
            model.AddressUrl = req.AddressURL;
            model.States = req.States;
            model.UserName = req.UserName;
            model.UserPwd = req.UserPwd;
            model.Remark = req.Remark;
            model.AnalogPacket = req.AnalogPacket;

            db.SaveChanges();
            return new AjaxResult<Object>("编辑成功！", 0);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AjaxResult<Object> DelSubchannel(long id)
        {
            if (id < 1)
            {
                return new AjaxResult<Object>("请选择您要删除的推广渠道信息！");
            }
            Subchannel model = db.Subchannel.Where(w => w.Id == id).FirstOrDefault();
            if (model == null)
            {
                return new AjaxResult<Object>("推广子平台渠道不存在！");
            }

            db.Subchannel.Remove(model);
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("删除成功！", 0);
            }
            return new AjaxResult<Object>("删除失败！");
        }

        /// <summary>
        /// 根据ID 查询子平台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Subchannel SelSubchannel(long id)
        {
            return db.Subchannel.Where(w => w.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据平台查询所有渠道
        /// </summary>
        /// <returns></returns>
        public List<Subchannel> GetAllList(long pid)
        {
            return db.Subchannel.Where(w => w.PlatformId == pid).OrderByDescending(w => w.Id).ToList();
        }

        /// <summary>
        /// 添加图片库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddJrttImagesinfo(Jrttimagesinfo model) 
        {
            db.Jrttimagesinfo.Add(model);
            return db.SaveChanges();
        }
    }
}
