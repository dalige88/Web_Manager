using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YL.Base;

namespace Web.Manager.Controllers
{
    public class ToolController : BaseController
    {
        #region Menu

        IConfiguration Configuration;
        IWebHostEnvironment Host;

        #endregion

        #region 页面

        public IActionResult Index()
        {
            return View();
        }





        #endregion

        #region Ajax 调用


        public async Task<AjaxResult<object>> UploadFile(string fileType)
        {
            IFormFile file1 = Request.Form.Files[0];
            string ext = "";
            List<string> typeList = new List<string>() { "image/gif", "image/jpeg", "image/png" };
            bool isImg = false;
            ImageFormat ifmat = ImageFormat.Png;
            if (file1.ContentType.Equals("image/gif"))
            {
                ext = ".gif";
                isImg = true;
                ifmat = ImageFormat.Gif;
            }
            else if (file1.ContentType.Equals("image/jpeg"))
            {
                ext = ".jpg";
                isImg = true;
                ifmat = ImageFormat.Jpeg;
            }
            else if (file1.ContentType.Equals("image/png"))
            {
                ext = ".png";
                isImg = true;
                ifmat = ImageFormat.Png;
            }
            else
            {
                return new AjaxResult<object>("只能上传图片", 1);
            }
            string pathfile = "";
            string filename = "";
            if (file1.Length > 0)
            {
                // var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                // string suffix = file1.FileName.Split('.')[1];
                string file = Guid.NewGuid() + ext;
                filename = file;
                pathfile = "/upload/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string path = Configuration["UploadPath"];

                string URLPath = "http://" + HttpContext.Request.Host.Value;
                //string URLPath = Configuration["FileDomain"];
                if (string.IsNullOrWhiteSpace(path))
                    path = Host.ContentRootPath + "/wwwroot" + pathfile;
                System.IO.Directory.CreateDirectory(path);
                pathfile += file;
                //物理路径
                var filePath = path + file;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file1.CopyToAsync(stream);
                }
                var data = new
                {
                    urlPath = URLPath + pathfile,
                    absPath = pathfile,
                    serverFilePath= filePath,
                };
                return new AjaxResult<object>() { data = data };
            }
            return new AjaxResult<object>();
        }



        #endregion
    }
}
