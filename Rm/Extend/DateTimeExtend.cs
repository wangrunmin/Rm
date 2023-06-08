using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Extend
{
    /// <summary>
    /// 日期对象的扩展方法
    /// </summary>
    public static class DateTimeExtend
    {
        /// <summary>
        /// 返回此时间对应的时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(this DateTime dateTime)
        {
            return Convert.ToInt64((dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        }
    }
}
