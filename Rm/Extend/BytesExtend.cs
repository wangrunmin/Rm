using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Extend
{
    /// <summary>
    /// 字节数组的扩展方法
    /// </summary>
    public static class BytesExtend
    {
        /// <summary>
        /// 用UTF8解码字节数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToUtf8Str(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
