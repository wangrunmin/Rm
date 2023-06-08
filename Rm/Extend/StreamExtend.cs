using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Rm.Extend
{
    /// <summary>
    /// 流的扩展方法
    /// </summary>
    public static class StreamExtend
    {
        /// <summary>
        /// 通过打开的文件流计算MD5
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetMD5Hash(this Stream stream)
        {
            var bytes = new MD5CryptoServiceProvider().ComputeHash(stream);
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
