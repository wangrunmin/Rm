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
    /// object的扩展方法
    /// </summary>
    public static class ObjectExtend
    {
        /// <summary>
        /// 新对象必须包含旧对象的所有属性，会直接将新对象的属性覆盖写入旧对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldObj"></param>
        /// <param name="newObj"></param>
        /// <returns></returns>
        public static object UpdateSelf<T>(this object oldObj, T newObj)
        {
            foreach (var propertyInfo in oldObj.GetType().GetProperties())
            {
                switch (propertyInfo.PropertyType.Name)
                {
                    case "Int32":
                        propertyInfo.SetValue(oldObj, (int)newObj.GetType().GetProperty(propertyInfo.Name).GetValue(newObj), null); break;
                    case "Int64":
                        propertyInfo.SetValue(oldObj, (long)newObj.GetType().GetProperty(propertyInfo.Name).GetValue(newObj), null); break;
                    case "Boolean":
                        propertyInfo.SetValue(oldObj, (bool)newObj.GetType().GetProperty(propertyInfo.Name).GetValue(newObj), null); break;
                    case "Decimal":
                        propertyInfo.SetValue(oldObj, (decimal)newObj.GetType().GetProperty(propertyInfo.Name).GetValue(newObj), null); break;
                    case "DateTime":
                        propertyInfo.SetValue(oldObj, (DateTime)newObj.GetType().GetProperty(propertyInfo.Name).GetValue(newObj), null); break;
                    case "String":
                        propertyInfo.SetValue(oldObj, newObj.GetType().GetProperty(propertyInfo.Name).GetValue(newObj)?.ToString(), null); break;
                    case "Nullable`1":
                        if (propertyInfo.PropertyType.FullName.Contains("DateTime"))
                        {
                            var newObjDateTime = newObj.GetType().GetProperty(propertyInfo.Name).GetValue(newObj);
                            if (newObjDateTime == null)
                            {
                                propertyInfo.SetValue(oldObj, null, null);
                            }
                            else
                            {
                                propertyInfo.SetValue(oldObj, (DateTime)newObjDateTime, null);
                            }

                            break;
                        }
                        else
                        {
                            throw new Exception("[请处理]未知的可空数据库类型：" + propertyInfo.PropertyType.FullName);
                        }

                    default:
                        throw new Exception("无法直接覆盖更新的对象类型：" + propertyInfo.PropertyType.Name);
                }
            }

            return oldObj;
        }

        /// <summary>
        /// 将对象序列化成json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将托管对象复制到非托管内存中
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IntPtr ConvertToIntPtr(this object obj)
        {
            int nSize = Marshal.SizeOf(obj);
            var ptrObj = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(obj, ptrObj, false);
            return ptrObj;
        }
    }
}
