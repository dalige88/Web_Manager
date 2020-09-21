using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            string str2 = Environment.CurrentDirectory;
            ViewBag.pyscript = str2 + "\\wwwroot\\PY\\发微头条图文皆可.py";

            return View();
        }

        [MenuItemAttribute("推广平台", "微头条管理", "添加微头条")]
        public IActionResult AddWttPage()
        {
            return View();
        }

        [MenuItemAttribute("推广平台", "微头条管理", "编辑微头条")]
        public IActionResult EditWttPage(long id)
        {
            Ypjrttweitoutiaoinfo model = wtt.Sel_Ypjrttweitoutiaoinfo(id);
            return View(model);
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
                model.PageIndex = req.PageIndex;
                model.PageSize = req.PageSize;
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
            if (string.IsNullOrWhiteSpace(req.Pid))
            {
                return Json(new AjaxResult<Object>("平台ID错误！"));
            }

            return Json(wtt.AddYPWTT(req));
        }

        /// <summary>
        /// 编辑微头条
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [MenuItemAttribute("推广平台", "微头条管理", "编辑微头条（提交）")]
        public JsonResult Ajax_EditWTT(JrttWeiTouTiaoReq req)
        {
            if (req.Id < 1)
            {
                return Json(new AjaxResult<Object>("请选择您要编辑的微头条！"));
            }
            if (string.IsNullOrWhiteSpace(req.Content))
            {
                return Json(new AjaxResult<Object>("微头条内容不能为空！"));
            }
            if (string.IsNullOrWhiteSpace(req.Pid))
            {
                return Json(new AjaxResult<Object>("平台ID错误！"));
            }

            return Json(wtt.EditYPWTT(req));
        }


        [MenuItemAttribute("推广平台", "微头条管理", "发布微头条到平台")]
        public JsonResult Ajax_PostWTT(string PYScript, long id)
        {
            if (id < 1)
            {
                return Json(new AjaxResult<Object>("信息错误！"));
            }
            if (string.IsNullOrWhiteSpace(PYScript))
            {
                return Json(new AjaxResult<Object>("脚本地址错误！"));
            }

            Ypjrttweitoutiaoinfo model = wtt.Sel_Ypjrttweitoutiaoinfo(id);
            if (model == null)
            {
                return Json(new AjaxResult<Object>("头条信息不存在！"));
            }


            //开始同步信息
            //string[] imgs = model.Images.Split(',');
            //base64加密
            string content = EncodeBase64("utf-8", model.Content);
            //string imgs= EncodeBase64("utf-8", model.Images);

            string script = PYScript + " " + model.Images + " " + content;
            //string script = "E:/work/NET/WebManager/WebManager/Web.Manager/wwwroot/PY/发微头条图文皆可.py  1111 123123";

            //base64解码
            //string ss = DecodeBase64("utf-8", content);

            //PY执行脚本
            return Ajax_PublicPostWTT(script, id);
        }


        #endregion
        /// <summary>
        /// PY执行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Ajax_PublicPostWTT(string script, long id)
        {
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
                        string jsonText = sr.ReadLine();
                        JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);

                        if (jo["status_code"].ToString() == "0")
                        {
                            wtt.UpYpStatus_YFB(id, (int)AIDB.Enum.JrttWeiTouTiaoEnum.status.头条平台已发布);
                            return Json(new AjaxResult<Object>("头条发布成功！", 0));

                        }
                        else
                        {
                            return Json(new AjaxResult<Object>("发布失败！"));
                        }


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

        ///编码（Base64方式的编码与解码）
        public string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        ///解码（Base64方式的编码与解码）
        public string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
    }
}
