using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AIDB.Models;
using AIServer;
using AIServer.Reqs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebManager.Core.Entity;
using YL.Base;

namespace Web.Manager.Controllers
{
    public class JRTTImagesController : BaseController
    {
        #region Menu

        JRTTImagesList jrtt;
        SubChannelList sh;
        JrttWeiTouTiaoList wtt;

        #endregion


        #region 页面

        [MenuItemAttribute("推广平台", "图片管理", "添加图片")]
        public IActionResult Add(long pid)
        {
            string str2 = Environment.CurrentDirectory;          //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //E:\work\NET\WebManager\WebManager\Web.Manager
            //E:/work/NET_Pro/ai_manager/Web_Manager/Web.Manager/wwwroot/PY/上传图片到材料库.py
            ViewBag.pyscript = str2 + "/wwwroot/PY/上传图片到材料库.py";

            ViewData["pid"] = pid;
            return View();
        }

        [MenuItemAttribute("推广平台", "图片管理", "图片详情")]
        public IActionResult Sell(long id)
        {
            return View(jrtt.Sell(id));
        }

        [MenuItemAttribute("推广平台", "图片管理", "图片列表")]
        public IActionResult ImagesList(long pid)
        {
            ViewData["pid"] = pid;
            object o = jrtt.GetList(pid);
            return View(o);
        }



        [MenuItemAttribute("推广平台", "微头条管理", "微头条列表")]
        public IActionResult WeiTouTiaoList()
        {
            return View();
        }

        [MenuItemAttribute("推广平台", "微头条管理", "添加微头条")]
        public IActionResult AddWttPage(long pid)
        {
            ViewBag.pid = pid;
            return View();
        }


        #endregion



        #region Ajax 调用

        [MenuItemAttribute("推广平台", "图片管理", "（上传）今日头条")]
        public JsonResult Ajax_AddJRTTImages(JrttimagesReq req)
        {
            /*string fileName = "shell/";
           fileName += "linux.sh";*/
            //var psi = new ProcessStartInfo("dotnet", "--info") { RedirectStandardOutput = true };
            string script = req.PYScript + " " + req.Url;

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
                        string jsonText = sr.ReadLine();
                        JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);

                        JrttimagesReq md = new JrttimagesReq();
                        md.PlatforminfoID = req.PlatforminfoID;
                        md.Url = req.Url;
                        md.Height = jo["height"].ToString();
                        md.Width = jo["width"].ToString();
                        md.WebUrl = jo["web_url"].ToString();
                        md.MimeType = jo["mime_type"].ToString();

                        return Json(jrtt.Add(md));
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

            return Json(new AjaxResult<Object>("上传头条服务器失败！"));

        }


        /// <summary>
        /// 微头条列表（已发布 / 未发布）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("推广平台", "微头条管理", "微头条列表")]
        public JsonResult Ajax_WTTGetList(JrttWeiTouTiaoReq req)
        {
            if (req.Status == (int)AIDB.Enum.JrttWeiTouTiaoEnum.status.头条平台已发布)
            {
                return Json(wtt.GetList(req));
            }
            else
            {
                YPJrttWeiTouTiaoReq model = new YPJrttWeiTouTiaoReq();
                model.Content = req.Content;
                return Json(wtt.YPGetList(model));
            }

        }

        /// <summary>
        /// 添加微头条
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("推广平台", "微头条管理", "添加微头条（提交）")]
        public JsonResult Ajax_AddWTT(JrttWeiTouTiaoReq req)
        {
            if (string.IsNullOrWhiteSpace(req.Content))
            {
                return Json(new AjaxResult<Object>("微头条内容不能为空！"));
            }
            if (req.Pid<1)
            {
                return Json(new AjaxResult<Object>("平台ID错误！"));
            }

            return Json(wtt.AddYPWTT(req));
        }



        #endregion


    }
}
