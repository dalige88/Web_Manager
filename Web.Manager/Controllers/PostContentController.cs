using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AIDB.Models;
using AIServer;
using AIServer.Reqs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebManager.Core.Entity;
using YL.Base;
using YL.Base.dtos;

namespace Web.Manager.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class PostContentController : BaseController
    {
        #region Menu
        PostContentList pc;
        PlatformList pl;

        #endregion

        #region 页面

        /// <summary>
        /// 文章管理列表页面
        /// </summary>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "添加")]
        public IActionResult AddPostContent()
        {
            List<Platforminfo> list= pl.GetAllList();
            return View(list);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "编辑")]
        public IActionResult EditPostContent(long id)
        {
            List<Platforminfo> list = pl.GetAllList();
            ViewBag.pl_list = list;

            var o = pc.SelPostcontent(id);
            return View(o);
        }

        #endregion

        #region Ajax调用
        /// <summary>
        /// 文章管理（获取）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "文章管理（获取）")]
        public JsonResult Ajax_GetList(PostContentReq req)
        {
            return Json(pc.GetList(req));
        }

        /// <summary>
        /// 添加文章（提交）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "添加文章（提交）")]
        public JsonResult Ajax_AddPostcontent(PostContentReq req)
        {
            if (string.IsNullOrWhiteSpace(req.MsgTitle))
            {
                return Json(new AjaxResult<Object>("请输入标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgContent))
            {
                return Json(new AjaxResult<Object>("请输入内容！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgAuthor))
            {
                return Json(new AjaxResult<Object>("请输入作者！"));
            }
            if (req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯文本 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯图片 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯视频 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.文本图片视频)
            {
                return Json(new AjaxResult<Object>("文章类型错误！"));
            }
            if (req.PlatformID<1)
            {
                return Json(new AjaxResult<Object>("推广平台信息错误！"));
            }
            if(req.SubChannelID<1)
            {
                return Json(new AjaxResult<Object>("推广渠道信息错误！"));
            }
            req.CreateManagerID = CurAccount.ManagerId;//当前管理员ID
            return Json(pc.AddPostcontent(req));
        }

        /// <summary>
        /// 编辑文章（提交）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "编辑文章（提交）")]
        public JsonResult Ajax_EditPostcontent(PostContentReq req)
        {
            if (req.ID<1)
            {
                return Json(new AjaxResult<Object>("请选择您要编辑的文章！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgTitle))
            {
                return Json(new AjaxResult<Object>("请输入标题！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgContent))
            {
                return Json(new AjaxResult<Object>("请输入内容！"));
            }
            if (string.IsNullOrWhiteSpace(req.MsgAuthor))
            {
                return Json(new AjaxResult<Object>("请输入作者！"));
            }
            if (req.OpenStatus!=(int)AIDB.Enum.PostContentEnum.OpenStatus.头条网已发布 && req.OpenStatus != (int)AIDB.Enum.PostContentEnum.OpenStatus.禁用)
            {
                return Json(new AjaxResult<Object>("启用状态错误！"));
            }
            if (req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯文本 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯图片 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯视频 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.文本图片视频)
            {
                return Json(new AjaxResult<Object>("文章类型错误！"));
            }

            return Json(pc.EditPostcontent(req));
        }

        /// <summary>
        /// 删除文章（提交）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "删除文章（提交）")]
        public JsonResult Ajax_DelPostcontent(long id)
        {
            if (id < 1)
            {
                return Json(new AjaxResult<Object>("请选择您要删除的文章！"));
            }
            return Json(pc.DelPostcontent(id));
        }

        /// <summary>
        /// 根据平台ID查询所有平台信息
        /// </summary>
        /// <returns></returns>
        public JsonResult Ajax_GetAllList()
        {
            List<Platforminfo> list = pl.GetAllList();

            return Json(new AjaxResult<List<Platforminfo>>(list));
        }


        [MenuItemAttribute("发帖管理", "文章管理", "发布头条平台")]
        public JsonResult Ajax_PostJRTTWenZhang(string PYScript, long id)
        {
            Postcontent model = pc.SelPostcontent(id);

            string script = PYScript + " " + model.MsgTitle + " " + model.MsgContent;

            /*var psi = new ProcessStartInfo("python", "E:/work/NET_Pro/ai_manager/Web_Manager/Web.Manager/wwwroot/PY/上传图片到材料库.py C:/Users/Administrator/Desktop/temp/1234.jpg") { RedirectStandardOutput = true };*/
            var psi = new ProcessStartInfo("python", script) { RedirectStandardOutput = true };
            var proc = Process.Start(psi);
            if (proc == null)
            {
                Console.WriteLine("Can not exec.");
            }
            else
            {
                Console.WriteLine("-------------Start read standard output--------------");
                //开始读取
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        //Console.WriteLine(sr.ReadLine());
                        //<img class="" src="http://p1.pstatp.com/origin/pgc-image/56e3500885e240ebb8901af9d7a2bb99" ic="undefined" image_type="" mime_type="" web_uri="pgc-image/56e3500885e240ebb8901af9d7a2bb99" img_width="640" img_height="640">
                        //{'code': 0, 'data': {'pgc_id': '6867566989301252622'}, 'err_no': 0, 'message': '提交成功', 'now': 1598980042, 'reason': '提交成功'}
                        string jsonText = sr.ReadLine();
                        JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);

                        if (jo["code"].ToString()=="0")
                        {
                            return Json(new AjaxResult<Object>("头条发布成功！",0));
                        }
                        else
                        {
                            return Json(new AjaxResult<Object>("发布失败！"));
                        }

                        //JrttimagesReq md = new JrttimagesReq();
                        //md.PlatforminfoID = req.PlatforminfoID;
                        //md.Url = req.Url;
                        //md.Height = jo["height"].ToString();
                        //md.Width = jo["width"].ToString();
                        //md.WebUrl = jo["web_url"].ToString();
                        //md.MimeType = jo["mime_type"].ToString();

                        //return Json(jrtt.Add(md));
                    }

                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                Console.WriteLine("---------------Read end------------------");
                Console.WriteLine($"Total execute time :{(proc.ExitTime - proc.StartTime).TotalMilliseconds} ms");
                Console.WriteLine($"Exited Code ： {proc.ExitCode}");

            }








            
            return Json(new AjaxResult<Object>("发布到头条失败！"));
        }


        #endregion
    }
}
