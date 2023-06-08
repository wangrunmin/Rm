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
    /// 不常用的，不便于分类的扩展方法
    /// </summary>
    public static class OtherExtend
    {
        /// <summary>
        /// 将一个海康的绝对时间坐标转换成DateTime对象
        /// </summary>
        /// <param name="utime"></param>
        /// <returns></returns>
        public static DateTime ConvertAbsTimeToDateTime(this uint utime)
        {
            var time = (int)utime;
            var year = (time >> 26) + 2000;
            var month = (time >> 22) & 15;
            var day = (time >> 17) & 31;
            var hour = (time >> 12) & 31;
            var minute = (time >> 6) & 63;
            var second = (time >> 0) & 63;
            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 输出prefix，耗时x.y秒
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetSecond(this Stopwatch stopwatch, string prefix)
        {
            return $"{prefix}，耗时{stopwatch.ElapsedMilliseconds / 1000}.{stopwatch.ElapsedMilliseconds % 1000}秒";
        }

        /// <summary>
        /// 释放非托管对象
        /// </summary>
        /// <param name="obj"></param>
        public static void FreeHGlobal(this IntPtr obj)
        {
            Marshal.FreeHGlobal(obj);
        }
    }
}
