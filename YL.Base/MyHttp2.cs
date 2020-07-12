using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CS.Base
{
    public class MyHttp2
    {
        /// <summary>
        /// 
        /// </summary>
        protected HttpClient http;
        /// <summary>
        /// 
        /// </summary>
        public MyHttp2()
        {
            var handler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true
            };

            http = new HttpClient(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public HttpResponseMessage HttpGet(string api, Dictionary<string, object> parameters = null, Dictionary<string, string> headers = null)
        {
            return HttpGetAsync(api, parameters, headers).Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual Task<HttpResponseMessage> HttpGetAsync(string api, Dictionary<string, object> parameters = null, Dictionary<string, string> headers = null)
        {

            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var queryString = string.Join("&", parameters.Where(p => p.Value == null || p.Value.GetType().IsValueType || p.Value.GetType() == typeof(string)).Select(p => string.Format("{0}={1}", Uri.EscapeDataString(p.Key), Uri.EscapeDataString(string.Format("{0}", p.Value)))));

            if (api.IndexOf("?") < 0)
            {
                api = string.Format("{0}?{1}", api, queryString);
            }
            else
            {
                api = string.Format("{0}&{1}", api, queryString);
            }

            api = api.Trim('&', '?');
            if (headers != null)
            {
                foreach (var h in headers)
                {
                    if (string.Compare(h.Key.ToLower(), "Content-Type".ToLower()) == 0)
                        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    else
                        http.DefaultRequestHeaders.Add(h.Key, h.Value);

                }
            }
            return http.GetAsync(api);
        }
        public string HttpPost(string api, string data)
        {
            return HttpPost(api, data, null).Content.ReadAsStringAsync().Result;
        }
        public HttpResponseMessage HttpPost(string api, string data, Dictionary<string, string> headers)
        {
            return HttpPostAsync(api, data, headers).Result;
        }
        public virtual Task<HttpResponseMessage> HttpPostAsync(string api, string data, Dictionary<string, string> headers)
        {

            HttpContent httpContent = null;

            var content = new StringContent(data);
            httpContent = content;

            if (headers != null)
            {
                foreach (var h in headers)
                {
                    if (string.Compare(h.Key.ToLower(), "Content-Type".ToLower()) == 0)
                        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    else
                        http.DefaultRequestHeaders.Add(h.Key, h.Value);

                }
            }
            return http.PostAsync(api, httpContent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public HttpResponseMessage HttpPost(string api, Dictionary<string, object> parameters, Dictionary<string, string> headers = null)
        {
            return HttpPostAsync(api, parameters, headers).Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual Task<HttpResponseMessage> HttpPostAsync(string api, Dictionary<string, object> parameters, Dictionary<string, string> headers)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            var dict = new Dictionary<string, object>(parameters.ToDictionary(k => k.Key, v => v.Value));

            HttpContent httpContent = null;

            if (dict.Count(p => p.Value.GetType() == typeof(byte[]) || p.Value.GetType() == typeof(System.IO.FileInfo)) > 0)
            {
                var content = new MultipartFormDataContent();

                foreach (var param in dict)
                {
                    var dataType = param.Value.GetType();
                    if (dataType == typeof(byte[]))	//byte[]
                    {
                        content.Add(new ByteArrayContent((byte[])param.Value), param.Key, GetNonceString());
                    }
                    //else if (dataType == typeof(MemoryFileContent))	//内存文件
                    //{
                    //    var mcontent = (MemoryFileContent)param.Value;
                    //    content.Add(new ByteArrayContent(mcontent.Content), param.Key, mcontent.FileName);
                    //}
                    else if (dataType == typeof(System.IO.FileInfo))	//本地文件
                    {
                        var file = (System.IO.FileInfo)param.Value;
                        content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(file.FullName)), param.Key, file.Name);
                        //TODO 加入文件类型
                        //获取资源，根据后者进行判断
                        //file.Extension
                        var v = YL.Base.Resource.ContentType.Replace("\r\n", "\n").Split('\n');
                        var t = v.Where(w => w.Split('=')[0].ToLower() == file.Extension.ToLower()).FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(t))
                            t = v[0];
                        content.First().Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(t.Split('=')[1]);
                    }
                    else /*if (dataType.IsValueType || dataType == typeof(string))*/	//其他类型
                    {
                        content.Add(new StringContent(string.Format("{0}", param.Value)), param.Key);
                    }


                }

                httpContent = content;

            }
            else
            {
                var content = new FormUrlEncodedContent(dict.ToDictionary(k => k.Key, v => string.Format("{0}", v.Value)));
                httpContent = content;
            }

            if (headers != null)
            {
                foreach (var h in headers)
                {
                    if (string.Compare(h.Key.ToLower(), "Content-Type".ToLower()) == 0)
                        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    else
                        http.DefaultRequestHeaders.Add(h.Key, h.Value);
                }
            }
            return http.PostAsync(api, httpContent);
        }
        private string GetNonceString(int length = 8)
        {
            var sb = new StringBuilder();

            var rnd = new Random();
            for (var i = 0; i < length; i++)
            {

                sb.Append((char)rnd.Next(97, 123));

            }

            return sb.ToString();

        }
    }
}
