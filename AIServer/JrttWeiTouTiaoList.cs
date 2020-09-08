using AIServer.Dtos;
using AIServer.Reqs;
using System;
using System.Collections.Generic;
using System.Text;
using YL.Base;
using YL.Base.Manager.Entity;
using System.Linq;
using AIDB.Models;

namespace AIServer
{
    public class JrttWeiTouTiaoList : AIServiceBase
    {
        public JrttWeiTouTiaoList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 原片  微头条  今日头条列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Pagination<YPJrttWeiTouTiaoDto>> YPGetList(YPJrttWeiTouTiaoReq req)
        {
            Pagination<YPJrttWeiTouTiaoDto> page = new Pagination<YPJrttWeiTouTiaoDto>();

            var query = from b in db.Ypjrttweitoutiaoinfo
                        select new YPJrttWeiTouTiaoDto
                        {
                            ID = b.Id,
                            Content = b.Content,
                            CreateTime = b.Createtime,
                            Images = b.Images,
                            PlatformID = b.PlatformId,
                            status = b.Status,
                        };
            if (!string.IsNullOrWhiteSpace(req.Content))
            {
                query = query.Where(w => w.Content.Contains(req.Content));
            }

            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ID).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<YPJrttWeiTouTiaoDto>>(page);
        }

        /// <summary>
        /// 已发布 微头条 今日头条
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Pagination<JrttWeiTouTiaoDto>> GetList(JrttWeiTouTiaoReq req)
        {
            Pagination<JrttWeiTouTiaoDto> page = new Pagination<JrttWeiTouTiaoDto>();

            var query = from b in db.Jrttweitoutiaoinfo
                        select new JrttWeiTouTiaoDto
                        {
                            ID = b.Id,
                            Content = b.Content,
                            Thread_Id = b.ThreadId,
                            Ugc_U13_Cut_Image_List = b.UgcU13CutImageList,
                            Publish_Time = b.PublishTime,
                        };
            if (!string.IsNullOrWhiteSpace(req.Content))
            {
                query = query.Where(w => w.Content.Contains(req.Content));
            }

            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ID).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<JrttWeiTouTiaoDto>>(page);
        }

        /// <summary>
        /// 添加微头条
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> AddYPWTT(JrttWeiTouTiaoReq req)
        {
            
            Ypjrttweitoutiaoinfo model = new Ypjrttweitoutiaoinfo();
            model.Content = req.Content;
            model.Createtime = DateTime.Now;
            model.Images = req.Images;
            model.PlatformId = req.Pid;
            model.Status = (int)AIDB.Enum.JrttWeiTouTiaoEnum.status.未发布;

            db.Ypjrttweitoutiaoinfo.Add(model);
            db.SaveChanges();
            return new AjaxResult<Object>("添加成功！", 0);
        }

        /// <summary>
        /// 根据ID 查询原片 头条内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ypjrttweitoutiaoinfo Sel_Ypjrttweitoutiaoinfo(long id) 
        {
            return db.Ypjrttweitoutiaoinfo.Where(w => w.Id == id).FirstOrDefault();
        }



    }
}
