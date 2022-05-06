using System.Collections.Generic;

namespace System.Collections.GenericEx
{
    public static class ListExtensions
    {
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        public static List<string> CreateStringIndxList(int beginvalue, int endvalue)
        {
            List<string> list = new List<string>();
            for(int i= beginvalue; i<=endvalue; ++i)
                list.Add(i.ToString());
            return list;
        }

        public static List<string> CreateStringIndxSqrList(int endvalue)
        {
            List<string> list = new List<string>();
            int sqr = 2;
            for (int i = 1; i <= endvalue; ++i)
            {
                sqr += 7;
                list.Add(sqr.ToString());
            }
            return list;
        }
    }

    public static class QueueExtensions
    {
        public static bool DequeueOne(this Queue<short> queue, short value)
        {
            int num = queue.Count;
            for (int i = 0; i < num; ++i)
            {
                short existValue = queue.Dequeue();
                if (existValue == value)
                    return true;
                queue.Enqueue(existValue);
            }
            return false;
        }

        public static bool DequeuesAfterOne(this Queue<short> queue, short value, bool removeValue = true)
        {
            bool found = false;
            int num = queue.Count;
            for (int i = 0; i < num; ++i)
            {
                short existValue = queue.Dequeue();
                if (found)
                    continue;
                if (existValue == value)
                {
                    found = true;
                    if (removeValue)
                        continue;
                }
                queue.Enqueue(existValue);
            }
            return found;
        }
    }
}
