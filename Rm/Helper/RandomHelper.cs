using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Helper
{
    /// <summary>
    /// 随机数辅助方法，避免每次使用都要实例化
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// 静态的随机数实例
        /// </summary>
        public static Random Random { get; set; } = new Random();

        /// <summary>
        /// 随机整数，[minValue,maxValue)
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        /// <summary>
        /// 随机正整数，[0,maxValue)
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        /// <summary>
        /// 随机正整数，[0,int.MaxValue)
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            return Random.Next();
        }

        /// <summary>
        /// 从数组中随机抽取一个结果
        /// </summary>
        /// <param name="ints"></param>
        /// <returns></returns>
        public static int GetRandomFromArray(int[] ints)
        {
            var index = Random.Next(ints.Length);
            return ints[index];
        }

        /// <summary>
        /// 从固定的随机数列表中抽取，new int[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 20, 30, 60, 120 }
        /// </summary>
        /// <returns></returns>
        public static int GetRandomTimeSpan()
        {
            var ints = new int[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 20, 30, 60, 120 };
            return GetRandomFromArray(ints);
        }

        /// <summary>
        /// 从固定的随机数列表中抽取，new int[] { 1, 1, 1, 1, 1, 1, 2, 3 }
        /// </summary>
        /// <returns></returns>
        public static int GetRandomRepeatTimes()
        {
            var ints = new int[] { 1, 1, 1, 1, 1, 1, 2, 3 };
            return GetRandomFromArray(ints);
        }
    }
}
