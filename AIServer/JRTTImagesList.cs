using AIDB.Models;
using AIServer.Reqs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YL.Base;
using YL.Base.Manager.Entity;

namespace AIServer
{
    /// <summary>
    /// 平台图库管理
    /// </summary>
    public class JRTTImagesList : AIServiceBase
    {
        public JRTTImagesList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 查询图片列表
        /// </summary>
        /// <param name="req">平台信息</param>
        /// <returns></returns>
        public List<Jrttimagesinfo> GetList(long pid)
        {
            Pagination<Jrttimagesinfo> page = new Pagination<Jrttimagesinfo>();
            return db.Jrttimagesinfo.Where(w => w.PlatforminfoId == pid).OrderByDescending(w => w.Id).ToList();

        }

        /// <summary>
        /// 添加图片信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public AjaxResult<Object> Add(JrttimagesReq req)
        {
            Jrttimagesinfo model = new Jrttimagesinfo();
            model.PlatforminfoId = req.PlatforminfoID;
            model.Url = req.Url;
            model.Height = req.Height;
            model.Width = req.Width;
            model.WebUrl = req.WebUrl;
            model.MimeType = req.MimeType;

            db.Jrttimagesinfo.Add(model);
            db.SaveChanges();
            return new AjaxResult<Object>("添加成功！", 0);

        }

        /// <summary>
        /// 根据ID查询图片详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Jrttimagesinfo Sell(long id) 
        {
            return db.Jrttimagesinfo.Where(w => w.Id == id).FirstOrDefault();
        }


    }
}
