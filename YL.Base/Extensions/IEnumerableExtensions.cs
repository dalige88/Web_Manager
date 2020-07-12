using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace YL.Base.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 转换为hashset集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        /// 集合拼接为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> source, string separator)
        {
            if (separator == null)
            {
                throw new ArgumentNullException(nameof(separator));
            }

            return string.Join<T>(separator, source);
        }

        /// <summary>
        /// 循环集合元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in list)
            {
                action(item);
            }
        }

        /// <summary>
        /// 转换只读集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IReadOnlyList<T> ToReadOnly<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return list.ToImmutableList();
        }

        /// <summary>
        /// 判断集合是否为null或者空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        /// <summary>
        /// 判断集合是否存在重复元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyComparer"></param>
        /// <returns></returns>
        public static bool HasRepeat<T>(this IEnumerable<T> list, IEqualityComparer<T> keyComparer = null)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            if (keyComparer == null)
                return list.GroupBy(r => r).Any(g => g.Count() > 1);
            else
                return list.GroupBy(r => r, keyComparer).Any(g => g.Count() > 1);
        }

        /// <summary>
        /// 判断集合是否存在重复的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="list"></param>
        /// <param name="selectProperty"></param>
        /// <param name="keyComparer"></param>
        /// <returns></returns>
        public static bool HasRepeat<T, P>(this IEnumerable<T> list, Func<T, P> selectProperty, IEqualityComparer<P> keyComparer = null)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (selectProperty == null)
            {
                throw new ArgumentNullException(nameof(selectProperty));
            }

            if (keyComparer == null)
                return list.GroupBy(selectProperty).Any(g => g.Count() > 1);
            else
                return list.GroupBy(selectProperty, keyComparer).Any(g => g.Count() > 1);
        }

        //public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> list, bool condition, Func<T, bool> where)
        //{
        //    if (list == null)
        //    {
        //        throw new ArgumentNullException(nameof(list));
        //    }

        //    if (where == null)
        //    {
        //        throw new ArgumentNullException(nameof(where));
        //    }

        //    if (condition)
        //        return list.Where(where);
        //    return list;
        //}

        //public static IQueryable<T> WhereIf<T>(this IQueryable<T> list, bool condition, Expression<Func<T, bool>> where)
        //{
        //    if (list == null)
        //    {
        //        throw new ArgumentNullException(nameof(list));
        //    }

        //    if (where == null)
        //    {
        //        throw new ArgumentNullException(nameof(where));
        //    }

        //    if (condition)
        //        return list.Where(where);
        //    return list;
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询源</param>
        /// <param name="pageIndex">当前页,索引从1开始</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        //public static IQueryable<T> Paging<T>(this IQueryable<T> query, PageInput input)
        //{
        //    if (query == null)
        //    {
        //        throw new ArgumentNullException(nameof(query));
        //    }

        //    return
        //        query
        //            .Skip((input.PageIndex - 1) * input.PageSize)
        //            .Take(input.PageSize);
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询源</param>
        /// <param name="pageIndex">当前页,索引从1开始</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return
                query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
        }

        /// <summary>
        /// 获取字典值,没有则返回默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key)
        {
            if (key == null)
                return default(TValue);
            if (dic.TryGetValue(key, out TValue value))
                return value;
            return default(TValue);
        }

        /// <summary>
        /// 是否为空行
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool IsEmptyRow(this DataRow row)
        {
            for (int j = 0; j < row.Table.Columns.Count; j++)
            {
                var value = row[j]?.ToString();
                if (!value.IsNullOrWhiteSpace())
                    return false;
            }
            return true;
        }
    }
}
