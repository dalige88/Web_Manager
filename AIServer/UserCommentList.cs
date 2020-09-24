using AIServer.Dtos;
using AIServer.Reqs;
using System;
using System.Collections.Generic;
using System.Text;
using YL.Base;
using YL.Base.Manager.Entity;
using System.Linq;

namespace AIServer
{
    /// <summary>
    /// 评论列表
    /// </summary>
    public class UserCommentList : AIServiceBase
    {
        public UserCommentList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 评论列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Pagination<UserCommentListDto>> GetUserCommentList(UserCommentListReq req)
        {
            Pagination<UserCommentListDto> page = new Pagination<UserCommentListDto>();
            var query = (from b in db.Usercommentlistinfo
                        select new UserCommentListDto
                        {
                            ID = b.Id,
                            ParentID = b.ParentId,
                            PlatformID = b.PlatformId,
                            CommentType = b.CommentType,
                            CommentTypeName = b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.短文 ? "短文" : b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.长文 ? "长文" : b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.视频 ? "视频" : "",
                            CommentTargetID = b.CommentTargetId,
                            CommentTargetTitle = b.CommentTargetTitle,
                            UserID = b.UserId,
                            UserAccount = b.UserAccount,
                            UserNice = b.UserNice,
                            CommentContent = b.ReplyContent,
                            CommentTime = b.CommentTime,
                            ReplyContent = b.ReplyContent,
                            ManagerID = b.ManagerId,
                            ManagerName = b.ManagerName,
                            ReplyTime = b.ReplyTime,
                            SignStatus = b.SignStatus,
                            SignStatusName = b.SignStatus== (int)AIDB.Enum.UserCommentListEnum.SignStatus.未标记 ? "未标记" : b.SignStatus == (int)AIDB.Enum.UserCommentListEnum.SignStatus.意向客户 ? "意向客户" : b.SignStatus == (int)AIDB.Enum.UserCommentListEnum.SignStatus.恶意评论 ? "恶意评论" :"",
                            Remark = b.Remark,

                        }).Where(w=>w.SignStatus==req.SignStatus);
            //父级ID
            if (req.ParentID>0)
            {
                query = query.Where(w => w.ParentID == req.ParentID);
            }
            //平台渠道ID
            if (req.PlatformID>0)
            {
                query = query.Where(w => w.PlatformID == req.PlatformID);
            }
            //评论对象类型：1：短文，2：长文：3：视频
            if (req.CommentType>0)
            {
                query = query.Where(w => w.CommentType == req.CommentType);
            }
            //评论对象标题
            if (!string.IsNullOrWhiteSpace(req.CommentTargetTitle))
            {
                query = query.Where(w => w.CommentTargetTitle.Contains(req.CommentTargetTitle));
            }

            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ID).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<UserCommentListDto>>(page);
        }


        public AjaxResult<Pagination<UserCommentListDto>> GetList(UserCommentListReq req)
        { }


        }
}
