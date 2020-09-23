using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        JRTTImagesController jc;
        SubChannelList sh;
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
        /// 文章内容加上（头条）图片所需要的参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string UpdateContent(string data)
        {
            //截取图片字段
            //获取截取的src
            //替换截取的图片字段
            //循环操作

            int num = 1;
            string dataes = "";


            string[] a1 = data.Split("_src=");
            foreach (var item in a1)
            {
                string items = "_src=";
                if (item.Contains("pgc-image/"))
                {
                    int b = item.IndexOf("pgc-image/");
                    int c = item.IndexOf(".jpg");
                    string a2 = item.Substring(b, c - b);

                    if (num > 1)
                    {
                        items = "web_uri=\"" + a2 + "\" _src=" + item;
                    }
                    else
                    {
                        items = item;
                    }
                }
                dataes += items;

                num++;
            }

            return dataes;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [MenuItemAttribute("发帖管理", "文章管理", "添加")]
        public IActionResult AddPostContent()
        {
            //PY执行脚本（获取和设置当前目录）
            string py_url = Environment.CurrentDirectory + "/wwwroot/PY/发布头条文章.py";
            ViewBag.pyscript = py_url;
            return View();
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
            if (string.IsNullOrWhiteSpace(req.PlatformIDs))
            {
                return Json(new AjaxResult<Object>("推广平台信息错误！"));
            }
            /* if (req.SubChannelID < 1)
             {
                 return Json(new AjaxResult<Object>("推广渠道信息错误！"));
             }*/
            if (string.IsNullOrWhiteSpace(req.HeadImg)||string.IsNullOrWhiteSpace(req.HeadImgServer))
            {
                return Json(new AjaxResult<Object>("请上传头像！"));
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

            if (req.ID < 1)
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
            if (req.OpenStatus != (int)AIDB.Enum.PostContentEnum.OpenStatus.已发布 && req.OpenStatus != (int)AIDB.Enum.PostContentEnum.OpenStatus.未发布)
            {
                return Json(new AjaxResult<Object>("启用状态错误！"));
            }
            if (req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯文本 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯图片 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.纯视频 && req.MsgType != (int)AIDB.Enum.PostContentEnum.MsgType.文本图片视频)
            {
                return Json(new AjaxResult<Object>("文章类型错误！"));
            }
            if (string.IsNullOrWhiteSpace(req.HeadImg) || string.IsNullOrWhiteSpace(req.HeadImgServer))
            {
                return Json(new AjaxResult<Object>("请上传首页头图！"));
            }
            if (string.IsNullOrWhiteSpace(req.PlatformIDs))
            {
                return Json(new AjaxResult<Object>("请选择发送渠道！"));
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
        public JsonResult Ajax_PostJRTTWenZhang(string PYScript, long id, string HeadImg)
        {
            if (id < 1)
            {
                return Json(new AjaxResult<Object>("信息错误！"));
            }
            if (string.IsNullOrWhiteSpace(PYScript))
            {
                return Json(new AjaxResult<Object>("脚本地址错误！"));
            }
            if (string.IsNullOrWhiteSpace(HeadImg))
            {
                return Json(new AjaxResult<Object>("请上传首页头图！"));
            }

            Postcontent model = pc.SelPostcontent(id);
            if (model == null)
            {
                return Json(new AjaxResult<Object>("文章不存在！"));
            }

            //循环查询脚本
            var pids = model.PlatformIds.Split(',');

            foreach (var item in pids)
            {
                Subchannel md = sh.SelSubchannel((long)Convert.ToInt32(item));
                if (md!=null)
                {
                    if (string.IsNullOrWhiteSpace(md.PyscriptLongEssay))
                    {
                        return Json(new AjaxResult<Object>("渠道《" + md.SubChannelName + "》，没有填写发布 长文 脚本！"));
                    }
                    if (string.IsNullOrWhiteSpace(md.AnalogPacket))
                    {
                        return Json(new AjaxResult<Object>("渠道COOKIE错误！"));
                    }

                    //base64加密（COOKIE）
                    string ck = EncodeBase64("utf-8", md.AnalogPacket);

                    //base64加密（内容）
                    string content = jc.EncodeBase64("utf-8", UpdateContent(model.MsgContent));

                    string script = PYScript + " " + model.HeadImg.Replace(" ", "") + " " + model.MsgTitle.Replace(" ", "") + " " + ck + " " + content;


                    //PY执行脚本
                    return Ajax_PublicPostWTT(script, id);

                }

            }

            return Json(new AjaxResult<Object>("发布完成！", 0));
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

                        if (jo["code"].ToString() == "0")
                        {
                            pc.UpOpenStatus(id, (int)AIDB.Enum.PostContentEnum.OpenStatus.已发布);
                            return Json(new AjaxResult<Object>("发布成功！", 0));

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

            return Json(new AjaxResult<Object>("发布失败！"));
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
