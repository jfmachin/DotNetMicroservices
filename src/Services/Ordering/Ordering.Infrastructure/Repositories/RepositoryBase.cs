﻿using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories {
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase {
        private readonly DbContext context;
        protected readonly DbSet<T> db;

        public RepositoryBase(DbContext context) {
            this.context = context;
            db = context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() {
            return await db.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate) {
            return await db.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true) {
            IQueryable<T> query = db;
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true) {
            IQueryable<T> query = db;
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id) {
            return await db.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity) {
            db.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity) {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity) {
            db.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
