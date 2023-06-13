using Microsoft.Win32;
using Newtonsoft.Json;
using Rm.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Rm.Extend
{
    /// <summary>
    /// 字符串的扩展方法
    /// </summary>
    public static class StringExtend
    {
        /// <summary>
        /// 通过默认浏览器打开链接
        /// </summary>
        /// <param name="url"></param>
        public static void OpenByBrowser(this string url)
        {
            var key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string defaultWebBrowserPath = key.GetValue("").ToString().Split(' ')[0];
            Process.Start(defaultWebBrowserPath, url);
        }

        /// <summary>
        /// 判断网络文件是否存在
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static bool HttpFileExist(this string fileUrl)
        {
            try
            {
                // 创建根据网络地址的请求对象
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(fileUrl));
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = 1000;

                // 返回响应状态是否是成功比较的布尔值
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 图片转Base64
        /// </summary>
        /// <param name="imageFileName">图片的完整路径</param>
        /// <returns></returns>
        public static string ImgToBase64(this string imageFileName)
        {
            try
            {
                using (var bmp = new Bitmap(imageFileName))
                {
                    using (var ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Jpeg);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 检查数组中是否有空的元素，有则为true，全部都有值才返回false
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static bool HaveNullOrEmpty(params string[] strArray)
        {
            foreach (var str in strArray)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 将yyyyMMddHHmmss格式的字符串转成datetime，如果失败则返回sql中最小的日期。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime YyyyMMddHHmmssToDateTime(this string str)
        {
            try
            {
                str = str.Substring(0, 4) + '-' + str.Substring(4, 2) + '-' + str.Substring(6, 2) + ' '
                 + str.Substring(8, 2) + ':' + str.Substring(10, 2) + ':' + str.Substring(12, 2);
                return Convert.ToDateTime(str);
            }
            catch (Exception)
            {
                return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            }
        }

        /// <summary>
        /// 非空的纯单词字符串，正则^[\w]+$
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsWord(this string str)
        {
            return new Regex(@"^[\w]+$").IsMatch(str);
        }

        /// <summary>
        /// 非空的数字、字母、下划线，正则^[a-zA-Z0-9_]+$
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumberLetterUnderLine(this string str)
        {
            return new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(str);
        }

        /// <summary>
        /// 非空的纯数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
        {
            return new Regex(@"^[0-9]+$").IsMatch(str);
        }

        /// <summary>
        /// 捕获异常的Convert.ToBoolean
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            try
            {
                return Convert.ToBoolean(str);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 捕获异常的Convert.ToInt32，失败则为0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 捕获异常的Convert.ToDecimal，失败则为0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str)
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换失败将返回当前时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            try
            {
                return Convert.ToDateTime(str);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 加盐的MD5加密算法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string ToEncryptMD5WithSalt(this string str, string salt = "YuanFeng")
        {
            var bytes = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str + salt));
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToUpper();
        }

        /// <summary>
        /// 加入复数形式，s或者ies或者es
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddPlural(this string str)
        {
            if (str.EndsWith("y"))
            {
                return str.Replace("y", "ies");
            }
            else if (str.EndsWith("ss"))
            {
                return str + "es";
            }
            else
            {
                return str + "s";
            }
        }

        /// <summary>
        /// 将json字符串进行反序列化，success表示序列化是否成功，items表示序列化后的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static (bool success, List<T> items) ToList<T>(this string str)
        {
            var list = new List<T>();
            try
            {
                list = JsonConvert.DeserializeObject<List<T>>(str);
                return (true, list);
            }
            catch (Exception)
            {
                return (false, list);
            }
        }

        /// <summary>
        /// 根据身份证获取<see cref="SEXTYPE"/>转换的int类型的性别，
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetSexByIDNumber(this string str)
        {
            return (int)(Convert.ToInt32(str.Substring(16, 1)) % 2 == 0 ? SEXTYPE.Female : SEXTYPE.Male);
        }

        /// <summary>
        /// 根据身份证获取出生日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime GetBirthDateByIDNumber(this string str)
        {
            return DateTime.Parse(str.Substring(6, 4) + "-" + str.Substring(10, 2) + "-" + str.Substring(12, 2));
        }

        /// <summary>
        /// 去除字符串中的所有空白字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAllEmpty(this string str)
        {
            return Regex.Replace(str, @"\s", string.Empty);
        }

        /// <summary>
        /// 根据文本生成图片，并返回base64类型的url。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GeneratePhotoBase64Url(this string text, int width = 960, int height = 540)
        {
            using (var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.DrawString(text, new Font("Consolas", 24), Brushes.Black, 10, 10);
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
    }
}
