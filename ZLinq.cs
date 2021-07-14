using System.Collections.Generic;

namespace ZNext
{
    public static unsafe class ZLinq
    {
        /// <summary>
        /// Zero allocation FirstOrDefault if IEnumerable is array or list
        /// </summary>
        /// <param name="collection">IEnumerable</param>
        /// <param name="predicate">pointer of predicate function</param>
        public static int ZFirstOrDefault(this IEnumerable<int> collection, delegate*<int, bool> predicate)
        {
            // optimization for array
            if (collection is int[] array)
            {
                return array.ArrayZFirstOrDefault(predicate);
            }

            // optimization for list
            if (collection is List<int> list)
            {
                return list.ListZFirstOrDefault(predicate);
            }
            
            foreach (var i in collection)
            {
                if (predicate(i))
                {
                    return i;
                }
            }

            return 0;
        }
        
        /// <summary>
        /// optimization of zero allocation FirstOrDefault for array
        /// </summary>
        private static int ArrayZFirstOrDefault(this int[] array, delegate*<int, bool> predicate)
        {
            int length = array.Length;
            int i = 0;
            while (i < length)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }

                i++;
            }

            return 0;
        }

        /// <summary>
        /// optimization of zero allocation FirstOrDefault for list
        /// </summary>
        private static int ListZFirstOrDefault(this List<int> list, delegate*<int, bool> predicate)
        {
            using var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    return enumerator.Current;
                }
            }

            return 0;
        }
    }
}