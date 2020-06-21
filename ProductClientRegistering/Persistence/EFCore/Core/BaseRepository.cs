using Domain.Core;
using Domain.Repository.Interface.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.EFCore.Core
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Base
    { 
        private bool _disposed;

        private readonly ProductClientContext _context;

        protected DatabaseFacade DataBase => _context.Database;

        public BaseRepository(ProductClientContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void SaveChanges(int userId = 1, string userName = "System", bool saveLog = true)
        {
            _context.SaveChanges();
        }

        protected TEntity ExecuteQuery(Expression<Func<TEntity, bool>> where = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool tracking = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.FirstOrDefault(where); ;
        }

        protected IEnumerable<TEntity> ExecuteQueryToList(Expression<Func<TEntity, bool>> where = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  int? skip = null,
                                  int? take = null,
                                  bool tracking = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
