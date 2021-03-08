using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using YL.Base;
using YL.Base.Manager.Entity;
using AIDB.Models;
using AIServer.Reqs;
using AIServer.Dtos;

namespace AIServer
{
    public class VideoYouTuBeList : AIServiceBase
    {
        public VideoYouTuBeList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 查询YouTuBe视频列表
        /// </summary>
        /// <param name="req">平台信息</param>
        /// <returns></returns>
        public AjaxResult<Pagination<VideoyoutubeListDto>> GetList(VideoYouTuBeListReq req)
        {
            Pagination<VideoyoutubeListDto> page = new Pagination<VideoyoutubeListDto>();
            var query = from b in db.Videoyoutube
                        select new VideoyoutubeListDto 
                        {
                            Id=b.Id,
                            YwTitle=b.YwTitle,
                            ZwTitle=b.ZwTitle,
                            Downloadurls=b.Downloadurls,
                            Localsrc=b.Localsrc,
                            Downloadtime=b.Downloadtime,
                            Posttime=b.Posttime,
                            Downloadstate=b.Downloadstate,
                            DownloadStateName=b.Downloadstate==(int)AIDB.Enum.VideoYouTuBeEnum.DownloadState.未下载? "未下载":
                            b.Downloadstate==(int)AIDB.Enum.VideoYouTuBeEnum.DownloadState.已下载? "已下载":
                            b.Downloadstate == (int)AIDB.Enum.VideoYouTuBeEnum.DownloadState.下载失败 ? "下载失败" :"",
                            Poststate =b.Poststate,
                            PostStateName = b.Poststate == (int)AIDB.Enum.VideoYouTuBeEnum.PostState.未发布 ? "未发布" :
                            b.Poststate == (int)AIDB.Enum.VideoYouTuBeEnum.PostState.已发布 ? "已发布" :
                            b.Poststate == (int)AIDB.Enum.VideoYouTuBeEnum.PostState.发布失败 ? "发布失败" : "",

                        };
            if (!string.IsNullOrWhiteSpace(req.yw_title))
            {
                query = query.Where(w => w.YwTitle.Contains(req.yw_title));
            }
            if (!string.IsNullOrWhiteSpace(req.zw_title))
            {
                query = query.Where(w => w.ZwTitle.Contains(req.zw_title));
            }
            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.Id).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<VideoyoutubeListDto>>(page);

        }

        /// <summary>
        /// 添加YouTuBe视频
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> AddVideoyoutube(VideoYouTuBeReq req)
        {
            Videoyoutube model_1 = db.Videoyoutube.Where(w => w.Downloadurls == req.downloadurls).FirstOrDefault();
            if (model_1 != null)
            {
                return new AjaxResult<Object>("该视频已存在！");
            }

            Videoyoutube model = new Videoyoutube();
            model.YwTitle = req.yw_title.Trim();
            model.ZwTitle = req.zw_title.Trim();
            model.Downloadurls = req.downloadurls.Trim();
            model.Downloadstate = (int)AIDB.Enum.VideoYouTuBeEnum.DownloadState.未下载;
            model.Poststate = (int)AIDB.Enum.VideoYouTuBeEnum.PostState.未发布;
            db.Videoyoutube.Add(model);
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("添加成功！", 0);
            }
            return new AjaxResult<Object>("添加失败！");
        }

        /// <summary>
        /// 编辑YouTuBe视频
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> EditVideoyoutube(VideoYouTuBeReq req)
        {
            Videoyoutube model_1 = db.Videoyoutube.Where(w => w.Id == req.ID).FirstOrDefault();
            if (model_1 == null)
            {
                return new AjaxResult<Object>("视频信息不存在！");
            }

            model_1.YwTitle = req.yw_title;
            model_1.ZwTitle = req.zw_title;
            model_1.Downloadurls = req.downloadurls;
            model_1.Downloadstate = req.downloadstate;
            model_1.Poststate = req.poststate;

            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("编辑成功！", 0);
            }
            return new AjaxResult<Object>("编辑失败！");


        }


        /// <summary>
        /// 查询YouTuBe视频详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VideoyoutubeListDto SelVideoyoutube(long id)
        {
            var query = from b in db.Videoyoutube
                        select new VideoyoutubeListDto 
                        {
                            Id = b.Id,
                            YwTitle = b.YwTitle,
                            ZwTitle = b.ZwTitle,
                            Downloadurls = b.Downloadurls,
                            Localsrc = b.Localsrc,
                            Downloadtime = b.Downloadtime,
                            Posttime = b.Posttime,
                            Downloadstate = b.Downloadstate,
                            DownloadStateName = b.Downloadstate == (int)AIDB.Enum.VideoYouTuBeEnum.DownloadState.未下载 ? "未下载" :
                            b.Downloadstate == (int)AIDB.Enum.VideoYouTuBeEnum.DownloadState.已下载 ? "已下载" :
                            b.Downloadstate == (int)AIDB.Enum.VideoYouTuBeEnum.DownloadState.下载失败 ? "下载失败" : "",
                            Poststate = b.Poststate,
                            PostStateName = b.Poststate == (int)AIDB.Enum.VideoYouTuBeEnum.PostState.未发布 ? "未发布" :
                            b.Poststate == (int)AIDB.Enum.VideoYouTuBeEnum.PostState.已发布 ? "已发布" :
                            b.Poststate == (int)AIDB.Enum.VideoYouTuBeEnum.PostState.发布失败 ? "发布失败" : "",
                        };

            return query.Where(w => w.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 删除视频信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AjaxResult<Object> DelVideoyoutube(long id) 
        {
            if (id<1)
            {
                return new AjaxResult<Object>("请选择您要删除的视频！");
            }
            Videoyoutube model_1 = db.Videoyoutube.Where(w => w.Id == id).FirstOrDefault();
            if (model_1 == null)
            {
                return new AjaxResult<Object>("视频信息不存在！");
            }
            db.Videoyoutube.Remove(model_1);
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("删除成功！", 0);
            }
            return new AjaxResult<Object>("删除失败！");
        }

    }
}
