using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddDigitalSignature
{
    public static class ExtensionMethods
    {

        public static void Clear(this BlockingCollection<string> blockingCollection)
        {
            try
            {
                if (blockingCollection == null)
                {
                    return;
                }

                while (blockingCollection.Count > 0)
                {
                    string item;
                    blockingCollection.TryTake(out item);
                    item = null;
                }

                blockingCollection.Dispose();
            }
            catch (System.Exception ex)
            {
                //cGlobalSettings.oLogger.WriteLogException("Clear", ex);
            }

        }


        public static void Clear(this BlockingCollection<int> blockingCollection)
        {
            try
            {
                if (blockingCollection == null)
                {
                    return;
                }

                while (blockingCollection.Count > 0)
                {
                    int item;
                    blockingCollection.TryTake(out item);
                }

                blockingCollection.Dispose();
            }
            catch (System.Exception ex)
            {
                //cGlobalSettings.oLogger.WriteLogException("Clear", ex);
            }

        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> @this, Func<T, TKey> keySelector)
        {
            return @this.GroupBy(keySelector).Select(grps => grps).Select(e => e.First());
        }


        public static Dictionary<TKey, TValue>
                 Merge<TKey, TValue>(Dictionary<TKey, TValue>[] dicts,
                                                           Func<IGrouping<TKey, TValue>, TValue> resolveDuplicates)
        {
            if (resolveDuplicates == null)
                resolveDuplicates = new Func<IGrouping<TKey, TValue>, TValue>(group => group.First());

            return dicts.SelectMany<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>(dict => dict)
                        .ToLookup(pair => pair.Key, pair => pair.Value)
                        .ToDictionary(group => group.Key, group => resolveDuplicates(group));
        }








        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> dicts,
                                                           Func<IGrouping<TKey, TValue>, TValue> resolveDuplicates)
        {
            if (resolveDuplicates == null)
                resolveDuplicates = new Func<IGrouping<TKey, TValue>, TValue>(group => group.First());

            return dicts.SelectMany<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>(dict => dict)
                        .ToLookup(pair => pair.Key, pair => pair.Value)
                        .ToDictionary(group => group.Key, group => resolveDuplicates(group));
        }

        public static long ToLong(long nFileSizeHigh, long nFileSizeLow)
        {
            return (long)((nFileSizeHigh << 0x20) | (nFileSizeLow & 0xffffffffL));

        }

        public static DateTime ToDateTime(this System.Runtime.InteropServices.ComTypes.FILETIME filetime)
        {
            long highBits = filetime.dwHighDateTime;
            highBits = highBits << 32;
            return DateTime.FromFileTimeUtc(highBits + (long)filetime.dwLowDateTime);
        }


        public static long ToLong(this System.Runtime.InteropServices.ComTypes.FILETIME filetime)
        {
            long highBits = filetime.dwHighDateTime;
            highBits = highBits << 32;
            return DateTime.FromFileTimeUtc(highBits + (long)filetime.dwLowDateTime).Ticks;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/419019/split-list-into-sublists-with-linq
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="groupSize"></param>
        /// <returns></returns>
        public static List<List<T>> Split<T>(this List<T> source, int groupSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / groupSize/*3*/)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        //        public static string SafeGetString(this SQLiteDataReader reader, int colIndex)
        //        {
        //            if (!reader.IsDBNull(colIndex))
        //                return reader.GetString(colIndex);
        //            else
        //                return string.Empty;
        //        }

        //        public static Int32 SafeGetInt32(this SQLiteDataReader reader, int colIndex, Int32 defaultReturnValue)
        //        {
        //            if (!reader.IsDBNull(colIndex))
        //                return reader.GetInt32(colIndex);
        //            else
        //                return defaultReturnValue;
        //        }

        //        public static Int64 SafeGetInt64(this SQLiteDataReader reader, int colIndex, Int64 defaultReturnValue)
        //        {
        //            if (!reader.IsDBNull(colIndex))
        //                return reader.GetInt64(colIndex);
        //            else
        //                return defaultReturnValue;
        //        }

        //        public static T SafeGetValue<T>(this SQLiteDataReader reader, int colIndex, T defaultReturnValue)
        //        {
        //            if (!reader.IsDBNull(colIndex))
        //            {
        //                if (typeof(T) == typeof(int))
        //                {
        //                    return (T)Convert.ChangeType(reader.GetInt32(colIndex), typeof(T)); 
        //                }
        //                else if (typeof(T) == typeof(long))
        //                {
        //                    return (T)Convert.ChangeType(reader.GetInt64(colIndex), typeof(T));
        //                }
        //                else if (typeof(T) == typeof(string))
        //                {
        //                    return (T)Convert.ChangeType(reader.GetString(colIndex), typeof(T)); 
        //                }
        //                else 
        //                {
        ////#error "not defined"
        //                    Debug.Assert(false);
        //                }


        //            }

        //            return defaultReturnValue;
        //        }

        public static List<Dictionary<T1, T2>> Split<T1, T2>(this Dictionary<T1, T2> source, int groupSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / groupSize/*3*/)
                .Select(x => x.Select(v => v.Value).ToDictionary(z => z.Key, z => z.Value))
                .ToList();
        }



        public static IEnumerable<string> Split(this string str, int n)
        {
            if (String.IsNullOrEmpty(str) || n < 1)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < str.Length; i += n)
            {
                yield return str.Substring(i, Math.Min(n, str.Length - i));
            }
        }
    }

    static class DataRowExtensions
    {
        public static object GetValueWithContainsCheck(this DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) ? row?[column] : null;
        }
    }
}
