using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Jieshai.Data
{
    public class DataProvider<T> where T : class
    {
        public virtual void Insert(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                session.Save(entity);
                session.Flush();
            }
        }

        /// <summary>
        /// wsest:删除数据
        /// </summary>
        /// <param name="t"></param>
        public void Delete(T t)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                session.Delete(t);
                session.Flush();
            }
        }

        public virtual void Update(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                session.Update(entity);
                session.Flush();
            }
        }
        public virtual T Get(object key)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<T>(key);
            }
        }

        #region HLinq

        public IQueryable<T> CreateQuery(ISession session)
        {
            var query = session.Query<T>();

            return query;
        }

        public IQueryable<T> CreateQuery(ISession session, Expression<Func<T, bool>> expression)
        {
            var query = session.Query<T>();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }

        public IQueryable<T> CreateQuery<Key>(ISession session, Expression<Func<T, Key>> orderBy, bool descending)
        {
            var query = session.Query<T>();
            if (orderBy != null)
            {
                if (descending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }

            return query;
        }

        public IQueryable<T> CreateQuery<Key>(ISession session, Expression<Func<T, bool>> expression, Expression<Func<T, Key>> orderBy, bool descending)
        {
            var query = this.CreateQuery(session, expression);

            if (orderBy != null)
            {
                if (descending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }

            return query;
        }

        public IQueryable<T> CreateQuery<Key>(ISession session, Expression<Func<T, bool>> expression, Expression<Func<T, Key>> orderBy, bool descending, int startIndex, int pageSize)
        {
            var query = this.CreateQuery(session, expression, orderBy, descending);

            
            query = query.Skip(startIndex).Take(pageSize);

            return query;
        }

        public IQueryable<T> CreateQuery<Key>(ISession session, Expression<Func<T, Key>> orderBy, bool descending, int startIndex, int pageSize)
        {
            var query = this.CreateQuery(session, orderBy, descending);

            
            query = query.Skip(startIndex).Take(pageSize);

            return query;
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<T> Query(Expression<Func<T, bool>> expression)
        {
            List<T> list = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                list = session.Query<T>()
                    .Where(expression)
                    .ToList();
            }

            return list;
        }


        /// <summary>
        /// 查询并排序
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public List<T> Query<Key>(System.Linq.Expressions.Expression<Func<T, Key>> orderBy, bool descending)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query = this.CreateQuery(session, orderBy, descending);

                return query.ToList();
            }
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<T>()
                    .Where(expression)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<Key>(Expression<Func<T, bool>> expression, System.Linq.Expressions.Expression<Func<T, Key>> orderBy, bool descending)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query = this.CreateQuery(session, expression, orderBy, descending);

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public List<T> Query<Key>(Expression<Func<T, bool>> expression, System.Linq.Expressions.Expression<Func<T, Key>> orderBy, bool descending)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query = this.CreateQuery(session, expression, orderBy, descending);

                return query.ToList();
            }
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> Query<Key>(Expression<Func<T, bool>> expression, System.Linq.Expressions.Expression<Func<T, Key>> orderBy, bool descending, int startIndex, int pageSize)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query = this.CreateQuery(session, expression, orderBy, descending, startIndex, pageSize);

                return query.ToList();
            }
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> Query<Key>(Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, Key>> orderBy, bool descending, int startIndex, int pageSize, out int totalCount)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query = this.CreateQuery(session, where);
                totalCount = query.Count();

                query = this.CreateQuery(session, where, orderBy, descending, startIndex, pageSize);
                return query.ToList();
            }
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> Query<Key>(System.Linq.Expressions.Expression<Func<T, Key>> orderBy, bool descending, int startIndex, int pageSize, out int totalCount)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query = this.CreateQuery(session);
                totalCount = query.Count();

                query = this.CreateQuery(session, orderBy, descending, startIndex, pageSize);
                return query.ToList();
            }
        }

        /// <summary>
        /// 通过expression查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> Query(int startIndex, int pageSize, out int totalCount)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query = session.Query<T>();
                totalCount = query.Count();

                query = session.Query<T>();
                
                return query.Skip(startIndex).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 查询记录数量
        /// </summary>
        /// <param name="expression">where条件</param>
        /// <returns></returns>
        public int QueryCount(Expression<Func<T, bool>> expression)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<T>().Where(expression).Count();
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="dirId">目录id</param>
        /// <returns></returns>
        public bool QueryAny(Expression<Func<T, bool>> expression)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<T>().Any(expression);
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="whereCriterion"></param>
        /// <param name="parameters"></param>
        public void Delete(Expression<Func<T, bool>> expression)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                session.Query<T>().Where(expression).Delete();
            }
        }

        #endregion
    }
}
