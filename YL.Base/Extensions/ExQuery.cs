using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace YL.Base.Extensions
{
    public static class ExQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            return source.GroupBy(keySelector).Select(x => x.FirstOrDefault());
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="CurPage">当前页数</param>
        /// <returns></returns>
        public static IEnumerable<TSource> QueryByPage<TSource>(this IEnumerable<TSource> source, int PageSize, int CurPage)
        {
            return source.Take(PageSize * CurPage).Skip(PageSize * (CurPage - 1));
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurPage"></param>
        /// <returns></returns>
        public static IQueryable<TSource> QueryByPage<TSource>(this IQueryable<TSource> source, int PageSize, int CurPage)
        {
            return source.Take(PageSize * CurPage).Skip(PageSize * (CurPage - 1));
        }
        /// <summary>
        /// 排序查询
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="item">排序的字段</param>
        /// <param name="order">排序类型:asc /desc</param>
        /// <returns></returns>
        public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, string item, string order)
        {
            if (order.ToLower() == "asc")
            {
                return source.OrderBy(o => o.GetType().GetProperty(item).GetValue(o, null));
            }
            else
            {
                return source.OrderByDescending(o => o.GetType().GetProperty(item).GetValue(o, null));
            }

        }
        /// <summary>
        /// 排序查询
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="item">排序的字段</param>
        /// <param name="order">排序类型:asc /desc</param>
        /// <returns></returns>
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string item, string order)
        {
            var param = Expression.Parameter(typeof(TSource));
            var body = Expression.Property(param, item);
            dynamic lambda = Expression.Lambda(body, param);
            if (order.ToLower() == "asc")
            {
                return Queryable.OrderBy(source, lambda);
            }
            else
            {
                return Queryable.OrderByDescending(source, lambda);
            }

        }
        /// <summary>
        /// 翻页查询
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="OffSet">跳过数量</param>
        /// <returns></returns>
        public static FlipPageResult<TSource> FlipPage<TSource>(this IEnumerable<TSource> source, int PageSize, int OffSet)
        {
            FlipPageResult<TSource> result = new FlipPageResult<TSource>();
            result.rows = source.Skip(OffSet).Take(PageSize).ToList();
            result.total = source.Count();
            return result;
        }
        /// <summary>
        /// 翻页查询
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="OffSet">跳过数量</param>
        /// <returns></returns>
        public static FlipPageResult<TSource> FlipPage<TSource>(this IQueryable<TSource> source, int PageSize, int OffSet)
        {
            FlipPageResult<TSource> result = new FlipPageResult<TSource>();
            result.rows = source.Skip(OffSet).Take(PageSize).ToList();
            result.total = source.Count();
            return result;
        }
        public static async System.Threading.Tasks.Task<FlipPageResult<TSource>> FlipPageAsync<TSource>(this IQueryable<TSource> source, int PageSize, int OffSet)
        {
            FlipPageResult<TSource> result = new FlipPageResult<TSource>();
            result.rows = await source.Skip(OffSet).Take(PageSize).ToListAsync();
            result.total = await source.CountAsync();
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="list"></param>
        /// <param name="condition"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> list, bool condition, Func<TSource, bool> where)
        {
            if (condition)
                return list.Where(where);
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="list"></param>
        /// <param name="condition"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> list, bool condition, Expression<Func<TSource, bool>> where)
        {
            if (condition)
                return list.Where(where);
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="preds"></param>
        /// <returns></returns>
        public static IQueryable<T> Any<T>(this IQueryable<T> source, Expression<Func<T, bool>>[] preds)
        {
            var par = Expression.Parameter(typeof(T), "x");

            Expression body = Expression.Constant(false);

            foreach (var pred in preds)
            {
                body = Expression.OrElse(body, Expression.Invoke(pred, par));
            }

            var ff = Expression.Lambda(body, par) as Expression<Func<T, bool>>;

            return source.Where(ff);
        }
        /// <summary>
        /// 多条件 or 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="preds"></param>
        /// <returns></returns>
        public static IQueryable<T> Any2<T>(this IQueryable<T> source, Expression<Func<T, bool>>[] preds)
        {
            Expression<Func<T, bool>> predicate = preds[0];
            for (int i = 1; i < preds.Length; i++)
                predicate = predicate.Or(preds[i]);
            return source.Where(predicate);
        }
        internal static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var expr2Body = new RebindParameterVisitor(expr2.Parameters[0], expr1.Parameters[0]).Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2Body), expr1.Parameters);
        }
    }

    internal class RebindParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public RebindParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node == _oldParameter)
            {
                return _newParameter;
            }

            return base.VisitParameter(node);
        }
    }
}
