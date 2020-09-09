﻿using AIDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using YL.Base;
using YL.Base.Manager.Entity;
using System.Linq;
using AIServer.Dtos;
using AIServer.Reqs;

namespace AIServer
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class PostContentList : AIServiceBase
    {
        public PostContentList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 查询发送数据包列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Pagination<PostContentDto>> GetList(PostContentReq req)
        {
            Pagination<PostContentDto> page = new Pagination<PostContentDto>();

            var query = from b in db.Postcontent
                        join c in db.Platforminfo on b.PlatformId equals c.Id
                        join d in db.Subchannel on b.SubChannelId equals d.Id
                        select new PostContentDto
                        {
                            ID = b.Id,
                            MsgContent = b.MsgContent,
                            CreateTime = b.CreateTime,
                            CreateManagerID = (long)b.CreateManagerId,
                            CreateManagerName = "",
                            CreateUserID = (long)b.CreateUserId,
                            CreateUserName = "",
                            CreateType = b.CreateType,
                            CreateTypeName = b.CreateType == (int)AIDB.Enum.PostContentEnum.CreateType.AI创建 ? "AI创建": b.CreateType == (int)AIDB.Enum.PostContentEnum.CreateType.人工创建 ? "人工创建" : "",
                            OpenStatus = b.OpenStatus,
                            OpenStatusName = b.OpenStatus == (int)AIDB.Enum.PostContentEnum.OpenStatus.头条网已发布 ? "头条网已发布" : b.OpenStatus == (int)AIDB.Enum.PostContentEnum.OpenStatus.未发布 ? "未发布" : "",
                            CreateUserType = b.CreateUserType,
                            CreateUserTypeName = b.CreateUserType == (int)AIDB.Enum.PostContentEnum.CreateUserType.用户 ? "用户" : b.OpenStatus == (int)AIDB.Enum.PostContentEnum.CreateUserType.管理员 ? "管理员" : "",
                            MsgType = b.MsgType,
                            MsgTypeName = b.MsgType == (int)AIDB.Enum.PostContentEnum.MsgType.文本图片视频 ? "文本图片视频" : b.MsgType == (int)AIDB.Enum.PostContentEnum.MsgType.纯图片 ? "图片" : b.MsgType == (int)AIDB.Enum.PostContentEnum.MsgType.纯文本 ? "文本" : b.MsgType == (int)AIDB.Enum.PostContentEnum.MsgType.纯视频 ? "视频" : "",
                            PlatformID = b.PlatformId,
                            PlatformName = c.PlatformName,
                            SubChannelID = b.SubChannelId,
                            SubChannelName = d.SubChannelName,
                            MsgTitle = b.MsgTitle,
                            MsgAuthor = b.MsgAuthor
                        };

            if (!string.IsNullOrWhiteSpace(req.MsgTitle))
            {
                query = query.Where(w => w.MsgTitle.Contains(req.MsgTitle));
            }
            if (!string.IsNullOrWhiteSpace(req.MsgAuthor))            
            {
                query = query.Where(w => w.MsgAuthor.Contains(req.MsgAuthor));
            }
            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ID).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<PostContentDto>>(page);

        }

        /// <summary>
        /// 添加要发送的帖子
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> AddPostcontent(PostContentReq req)
        {
            Postcontent model_1 = db.Postcontent.Where(w => w.MsgTitle == req.MsgTitle).FirstOrDefault();
            if (model_1 != null)
            {
                return new AjaxResult<Object>("此帖子已经发送过，请不要重复发送！");
            }

            Postcontent model = new Postcontent();
            model.MsgTitle = req.MsgTitle.Trim();
            model.MsgContent = req.MsgContent.Trim();
            model.MsgAuthor = req.MsgAuthor.Trim();
            model.CreateTime = DateTime.Now;
            model.CreateManagerId = req.CreateManagerID;
            model.CreateUserId = req.CreateUserID;
            model.CreateType = (int)AIDB.Enum.PostContentEnum.CreateType.人工创建;
            model.OpenStatus = (int)AIDB.Enum.PostContentEnum.OpenStatus.头条网已发布;
            model.CreateUserType = (int)AIDB.Enum.PostContentEnum.CreateUserType.管理员;
            model.MsgType = req.MsgType;
            model.PlatformId = req.PlatformID;
            model.SubChannelId = req.SubChannelID;
            model.HeadImg = req.HeadImg;
            model.HeadImgServer = req.HeadImgServer;
            db.Postcontent.Add(model);

            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("添加成功！", 0);
            }
            return new AjaxResult<Object>("添加失败！");
        }

        /// <summary>
        /// 编辑要发送的帖子
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> EditPostcontent(PostContentReq req)
        {
            Postcontent model = db.Postcontent.Where(w => w.Id == req.ID).FirstOrDefault();
            if (model == null)
            {
                return new AjaxResult<Object>("您编辑的帖子信息不存在！");
            }

            model.MsgTitle = req.MsgTitle.Trim();
            model.MsgContent = req.MsgContent.Trim();
            model.MsgAuthor = req.MsgAuthor.Trim();
            model.HeadImg = req.HeadImg.Trim();
            model.HeadImgServer = req.HeadImgServer.Trim();
            //model.CreateTime = req.CreateTime;
            //model.CreateManagerId = req.CreateManagerID;
            //model.CreateUserId = req.CreateUserID;
            //model.CreateType = req.CreateType;
            model.OpenStatus = req.OpenStatus;
            //model.CreateUserType = req.CreateUserType;
            model.MsgType = req.MsgType;
            //model.PlatformId = req.PlatformID;
            //model.SubChannelId = req.SubChannelID;
            db.SaveChanges();
            return new AjaxResult<Object>("编辑成功！", 0);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AjaxResult<object> DelPostcontent(long id) 
        {
            if (id < 1)
            {
                return new AjaxResult<Object>("请选择您要删除的帖子信息！");
            }
            Postcontent model = db.Postcontent.Where(w => w.Id == id).FirstOrDefault();
            if (model==null)
            {
                return new AjaxResult<Object>("帖子信息不存在！");
            }
            db.Postcontent.Remove(model);
            if (db.SaveChanges() > 0)
            {
                return new AjaxResult<Object>("删除成功！", 0);
            }
            return new AjaxResult<Object>("删除失败！");
        }

        /// <summary>
        /// 根据ID查询文章详情`
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Postcontent SelPostcontent(long id) 
        {
            return db.Postcontent.Where(w => w.Id == id).FirstOrDefault();
        }


        /// <summary>
        /// 修改  是否发布状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public int UpOpenStatus(long id,int status) 
        {
            Postcontent model = db.Postcontent.Where(w => w.Id == id).FirstOrDefault();
            if (model!=null)
            {
                model.OpenStatus = status;
                return db.SaveChanges();
            }

            return 0;
        }
    }
}
