using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace YL.Base.Common
{

    /// <summary>
    /// 加密
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(strText);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
        /// <summary>
        /// MD5加密16位
        /// </summary>
        /// <param name="strText"></param>
        /// <returns>大写</returns>
        public static string MD5Encrypt16(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string str = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(strText)), 4, 8);
            md5.Clear();
            return str;
        }
        /// <summary>
        /// 密码MD5加密
        /// </summary>
        /// <param name="PassWord">页面传入MD5一次的密码</param>
        /// <param name="Salt"></param>
        /// <returns>MD5加密后的密码</returns>
        public static string PassWordEncrypt(string PassWord, string Salt)
        {
            string str = PassWord + Salt;
            str = MD5Encrypt(str);
            return str;
        }
        /// <summary>
        /// 密码MD5加密,特殊处理
        /// </summary>
        /// <param name="PassWord">页面传入MD5一次的密码</param>
        /// <param name="Salt"></param>
        /// <returns>MD5加密后的密码</returns>
        public static string PassWordEncrypt2(string PassWord, string Salt)
        {
            if (PassWord.Length < 6)
                throw new Exception("密码长度不能小于6位");

            string str = string.Format("{0}{1}{2}", PassWord.Substring(0, 3), Salt, PassWord.Substring(4));
            str = MD5Encrypt(str);
            return str;
        }
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public string StringEncode(string data)
        {
            byte[] bpath = System.Text.Encoding.Default.GetBytes(data);
            List<byte> list = new List<byte>();
            foreach (byte v in bpath)
            {
                var c = v ^ 0x34;
                list.Add((byte)(c ^ 0x76));
            }
            return Convert.ToBase64String(list.ToArray());
        }
        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>解密结果【1】</returns>
        static public List<string> StringDecode(string data)
        {
            try
            {
                byte[] bpath = Convert.FromBase64String(data);
                List<byte> list = new List<byte>();
                List<byte> list2 = new List<byte>();
                List<byte> list3 = new List<byte>();
                foreach (byte v in bpath)
                {
                    var c = v ^ 0x34;
                    var d = v ^ 0x03;
                    var e = v ^ 0x1f;
                    list.Add((byte)(c ^ 0x76));
                    list2.Add((byte)(d ^ 0x76));
                    list3.Add((byte)(e ^ 0x76));
                }
                string str = System.Text.Encoding.Default.GetString(list.ToArray());
                string str2 = Convert.ToBase64String(list2.ToArray(), 0, list2.Count);
                string str3 = Convert.ToBase64String(list3.ToArray(), 0, list3.Count);
                return new List<string>() { str2, str, str3 };
            }
            catch
            {
                return new List<string>() { "", data, "" };
            }
        }
    }
}
