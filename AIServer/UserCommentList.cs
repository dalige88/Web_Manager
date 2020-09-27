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
        /// 用户评论列表
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
                             UserCommentTargetInfoID = b.CommentTargetId,
                             ParentID = b.ParentId,
                             /*PlatformID = b.PlatformId,
                             CommentType = b.CommentType,
                             CommentTypeName = b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.短文 ? "短文" : b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.长文 ? "长文" : b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.视频 ? "视频" : "",
                             CommentTargetID = b.CommentTargetId,
                             CommentTargetTitle = b.CommentTargetTitle,*/
                             UserID = b.UserId,
                             UserAccount = b.UserAccount,
                             UserNice = b.UserNice,
                             CommentContent = b.CommentContent,
                             CommentTime = b.CommentTime,
                             ReplyContent = b.ReplyContent,
                             ManagerID = b.ManagerId,
                             ManagerName = b.ManagerName,
                             ReplyTime = b.ReplyTime,
                             SignStatus = b.SignStatus,
                             SignStatusName = b.SignStatus == (int)AIDB.Enum.UserCommentListEnum.SignStatus.未标记 ? "未标记" : b.SignStatus == (int)AIDB.Enum.UserCommentListEnum.SignStatus.意向客户 ? "意向客户" : b.SignStatus == (int)AIDB.Enum.UserCommentListEnum.SignStatus.恶意评论 ? "恶意评论" : "",
                             Remark = b.Remark,
                             dongtai_id = b.DongtaiId,
                             CommentTargetID = b.CommentTargetId,
                             CommentTargetTitle = db.Usercommenttargetinfo.Where(s=>s.CommentTargetId==b.CommentTargetId).FirstOrDefault().CommentTargetTitle,

                         }).Where(w => w.UserCommentTargetInfoID == req.UserCommentTargetInfoID && w.SignStatus == req.SignStatus);
            //父级ID
            if (req.ParentID > 0)
            {
                query = query.Where(w => w.ParentID == req.ParentID);
            }


            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ID).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<UserCommentListDto>>(page);
        }

        /// <summary>
        /// 查询用户评论对象列表（根据作者查询）？？？？？
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Pagination<UserCommentTargetListDto>> GetList(UserCommentTargetListReq req)
        {
            Pagination<UserCommentTargetListDto> page = new Pagination<UserCommentTargetListDto>();
            var query = from b in db.Usercommenttargetinfo
                        select new UserCommentTargetListDto
                        {
                            ID = b.Id,
                            PlatformID = b.PlatformId,
                            PlatformName = db.Subchannel.Where(w => w.Id == b.PlatformId).FirstOrDefault().SubChannelName,
                            CommentType = b.CommentType,
                            CommentTypeName = b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.短文 ? "短文" : b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.长文 ? "长文" : b.CommentType == (int)AIDB.Enum.UserCommentListEnum.CommentType.视频 ? "视频" : "",
                            CommentTargetID = b.CommentTargetId,
                            CommentTargetTitle = b.CommentTargetTitle,
                            Remark = b.Remark,
                            TableName = b.TableName,
                            SoureUrl = b.SoureUrl,
                            PinLunCount = db.Usercommentlistinfo.Where(s => s.CommentTargetId == b.CommentTargetId).Count(),
                        };

            if (req.PlatformID > 0)
            {
                query = query.Where(w => w.PlatformID == req.PlatformID);
            }
            if (req.CommentType > 0)
            {
                query = query.Where(w => w.CommentType == req.CommentType);
            }
            if (!string.IsNullOrWhiteSpace(req.CommentTargetTitle))
            {
                query = query.Where(w => w.CommentTargetTitle.Contains(req.CommentTargetTitle));
            }

            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ID).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            return new AjaxResult<Pagination<UserCommentTargetListDto>>(page);
        }



    }
}
