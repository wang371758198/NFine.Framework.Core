using System;
using System.Collections.Generic;
using System.Text;

namespace NFine.Code
{
    /// <summary>
    /// MD5加密
    /// </summary>
   public  class Md5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                byte [] bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
                strEncrypt = System.Text.Encoding.UTF8.GetString(bytes).Substring(8,16);
            }

            if (code == 32)
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                byte[] bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
                strEncrypt = System.Text.Encoding.UTF8.GetString(bytes);
            }

            return strEncrypt;
        }
    }
}
