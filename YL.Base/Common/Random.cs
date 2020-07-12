using System;

namespace YL.Base.Common
{
    public class RandomNumber
    {
        /// <summary>
        /// 获取时间戳,UTC时间-1970初始时间
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            //System.Threading.Thread.Sleep(1);//阻塞线程0.001秒
            String str = ts.Ticks.ToString();
            str = str.Substring(5);
            return str;
        }
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns>随机数</returns>
        public static String GetRandomCode(int length)
        {
            if (true)
            {
                int blen = 1;
                blen *= (length / 3 + 1);
                // Create a byte array to hold the random value.  
                byte[] randomNumber = new byte[blen];
                // Create a new instance of the RNGCryptoServiceProvider.  
                System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                // Fill the array with a random value.  
                rng.GetBytes(randomNumber);
                // Convert the byte to an uint value to make the modulus operation easier.  
                uint randomResult = 0x0;
                for (int i = 0; i < blen; i++)
                {
                    randomResult |= ((uint)randomNumber[i] << ((blen - 1 - i) * 8));
                }
                string s = randomResult.ToString();
                if (s.Length > length)
                    s = s.Substring(0, length);
                while (s.Length < length)
                {
                    s = "0" + s;
                }
                return s;
            }
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            System.Random rd = new System.Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(rd.Next(10));
            }
            return newRandom.ToString();

        }
        /// <summary>
        /// 在区间[minValue,maxValue]取出num个互不相同的随机数，返回数组。
        /// </summary>
        /// <param name="num"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int[] GetRandomNum(int num, int minValue, int maxValue)
        {
            System.Random ra = new System.Random(unchecked((int)DateTime.Now.Ticks));//保证产生的数字的随机性
            int[] arrNum = new int[num];
            int tmp = 0;
            for (int i = 0; i <= num - 1; i++)
            {
                tmp = ra.Next(minValue, maxValue); //随机取数 
                arrNum[i] = GetNum(arrNum, tmp, minValue, maxValue, ra); //取出值赋到数组中 
            }
            return arrNum;
        }
        /// <summary>
        /// 取出值赋到数组中 
        /// </summary>
        /// <param name="arrNum"></param>
        /// <param name="tmp"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="ra"></param>
        /// <returns></returns>
        public static int GetNum(int[] arrNum, int tmp, int minValue, int maxValue, System.Random ra)
        {
            int n = 0;
            while (n > arrNum.Length - 1)
            {
                if (arrNum[n] == tmp) //利用循环判断是否有重复
                {
                    tmp = ra.Next(minValue, maxValue); //重新随机获取。
                    GetNum(arrNum, tmp, minValue, maxValue, ra); //递归:如果取出来的数字和已取得的数字有重复就重新随机获取。
                }
                n++;
            }
            return tmp;
        }
        /// <summary>
        /// 生成订单号，生成时间戳+3位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNum()
        {
            return GetTimeStamp() + GetRandomCode(3);
        }
    }
}
