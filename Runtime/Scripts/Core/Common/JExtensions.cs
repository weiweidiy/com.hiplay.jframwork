using System;
using System.Collections.Generic;
using System.Linq;

namespace JFramework
{
    public static class JExtensions
    {
        /// <summary>
        /// 返回随机打乱的列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> GetRandomElements<T>(this List<T> source, int count)
        {
            return source.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
        }

        /// <summary>
        /// 2分排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static List<T> BinarySort<T>(this List<T> lst, IComparer<T> comparer)
        {
            T temp;
            int j, mid;
            for (int i = 0; i < lst.Count; i++)
            {
                temp = lst[i];
                //把temp当成key，利用二分查找法，找到temp需要插入的位置
                int left = 0;
                int right = i - 1; //取i之前的数组

                while (left <= right)
                {
                    mid = (right - left) / 2 + left;

                    //if (temp > arr[mid])
                    if (comparer.Compare(temp, lst[mid]) > 0)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                //通过二分查找法找到temp需要插入的位置left。
                for (j = i - 1; j >= left; j--)
                {
                    lst[j + 1] = lst[j];
                }
                //把temp 赋值给left的位置
                lst[left] = temp;
            }

            return lst;
        }

        /// <summary>
        /// 弹出第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T PopFirst<T>(this List<T> list)
        {
            if (list.Count == 0) throw new InvalidOperationException("List is empty");
            T item = list[0];
            list.RemoveAt(0);
            return item;
        }
    }


}


