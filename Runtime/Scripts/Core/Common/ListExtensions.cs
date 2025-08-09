using System;
using System.Collections.Generic;
using System.Linq;

namespace JFramework
{
    public static class ListExtensions
    {
        /// <summary>
        /// ��ȡ�б��е����Ԫ��
        /// </summary>
        public static T GetRandomItem<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("�б���Ϊ��", nameof(list));

            Random random = new Random();
            int index = random.Next(list.Count);
            return list[index];
        }

        /// <summary>
        /// ��ȡ�б��е����Ԫ�أ�֧���޷Żغ��зŻأ�
        /// </summary>
        public static List<T> GetRandomItems<T>(this List<T> list, int count)
        {
            if (list == null || list.Count == 0 || count <= 0)
                return new List<T>();

            List<T> result = new List<T>(count);
            Random random = new Random();

            if (count <= list.Count)
            {
                // �޷Żس�ȡ
                List<T> tempList = new List<T>(list);
                for (int i = 0; i < count; i++)
                {
                    int index = random.Next(tempList.Count);
                    result.Add(tempList[index]);
                    tempList.RemoveAt(index);
                }
            }
            else
            {
                // �зŻس�ȡ
                for (int i = 0; i < count; i++)
                {
                    int index = random.Next(list.Count);
                    result.Add(list[index]);
                }
            }

            return result;
        }

        /// <summary>
        /// �� List ���ж�������
        /// </summary>
        public static void BinarySort<T>(this List<T> lst, IComparer<T> comparer)
        {
            T temp;
            int j, mid;
            for (int i = 0; i < lst.Count; i++)
            {
                temp = lst[i];
                int left = 0;
                int right = i - 1;
                while (left <= right)
                {
                    mid = (right - left) / 2 + left;
                    if (comparer.Compare(temp, lst[mid]) > 0)
                        left = mid + 1;
                    else
                        right = mid - 1;
                }
                for (j = i - 1; j >= left; j--)
                {
                    lst[j + 1] = lst[j];
                }
                lst[left] = temp;
            }
        }

        /// <summary>
        /// �� List ���ж��ֲ��ң�����Ŀ��Ԫ��������Ӧ����λ��
        /// </summary>
        public static int BinarySearch<T>(this List<T> sortedList, T target, IComparer<T> comparer)
        {
            int left = 0;
            int right = sortedList.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (comparer.Compare(sortedList[mid], target) == 0)
                    return mid;
                if (comparer.Compare(sortedList[mid], target) < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return left;
        }

        /// <summary>
        /// ���ֲ��뵽 List
        /// </summary>
        public static void BinarySearchInsert<T>(this List<T> sortedList, T target, IComparer<T> comparer)
        {
            int left = 0;
            int right = sortedList.Count - 1;
            int result = 0;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (comparer.Compare(sortedList[mid], target) == 0)
                    result = mid;
                if (comparer.Compare(sortedList[mid], target) < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            result = left;
            sortedList.Insert(result, target);
        }

        /// <summary>
        /// ������һ��Ԫ��
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

        ///// <summary>
        ///// �����ȡ�б��еĶ��Ԫ��
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="source"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public static List<T> GetRandomElements<T>(this List<T> source, int count)
        //{
        //    return source.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
        //}
    }
}