using Emenu.Core.Const;
using Emenu.Core.Repositoryies;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.DAL.Repositoryies
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class 
    {

        protected ApplicationDbContext _context;
        private readonly DbSet<T> _model;

        public BaseRepository(ApplicationDbContext Context)
        {
            _context = Context;
            _model = _context.Set<T>();
        }
        public async Task<ErrorOr<int>> GetCountAsync(Expression<Func<T, bool>> query = null)
        {
            try
            {
                return await _model.CountAsync(query);
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }
        public async Task<List<TResult>> FindListAsync<TResult>(Expression<Func<T, bool>>? filters,
            Expression<Func<T, TResult>> selectExpression,
            int? take,
            int? skip,
            Expression<Func<T, object>>? orderBy = null,
            string orderByDirction = "ASC",
            string[]? includes = null,
            bool? distinct = null,
            bool asTracking = false
            )
        {
            List<TResult> result;
            IQueryable<T> query = _model.AsQueryable();
            //query = query.AsQueryable();
            if (!asTracking)
                query.AsNoTracking();

            if (includes != null && includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (filters != null)
            {
                //foreach (var filter in filters)
                //{
                query = query.Where(filters);
                //}
            }
            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirction == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);

                }
            }

            if (distinct != null)
                query = query.Distinct();

            //if (selectExpression != null)
            //{
            //    query = query.Select(selectExpression);
            //}

            result = await query.Select(selectExpression).ToListAsync();
            return result;
        }

        public async Task<List<T>> FindListAsync(Expression<Func<T, bool>>? filters,
            int? take,
            int? skip,
            Expression<Func<T, object>>? orderBy = null,
            string orderByDirction = "ASC",
            string[]? includes = null,
            bool? distinct = null,
            bool asTracking = false
            )
        {

            IQueryable<T> query = _model.AsQueryable();
            //query = query.AsQueryable();
            if (!asTracking)
                query.AsNoTracking();

            if (includes != null && includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (filters != null)
            {
                //foreach (var filter in filters)
                //{
                query = query.Where(filters);
                //}
            }
            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirction == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);

                }
            }

            if (distinct != null)
                query = query.Distinct();

            //if (selectExpression != null)
            //{
            //    query = query.Select(selectExpression);
            //}

            return await query.ToListAsync();
        }

        public async Task<ErrorOr<TResult>> FindSingleAsync<TResult>(Expression<Func<T, bool>>? filters,
            Expression<Func<T, TResult>> selectExpression,
            string[]? includes = null,
            bool asTracking = false
            )
        {
            try
            {
                TResult result;
                IQueryable<T> query = _model.AsQueryable();
                if (!asTracking)
                    query.AsNoTracking();

                if (includes != null && includes.Count() > 0)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                query = query.Where(filters);

                result = await query.Select(selectExpression).FirstOrDefaultAsync();

                if (result == null)
                    return Error.Failure(description: "Not Found Data");

                return result;
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }

        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>>? filters,
            string[]? includes = null,
            bool asTracking = false
            )
        {
            IQueryable<T> query = _model.AsQueryable();
            if (!asTracking)
                query.AsNoTracking();

            if (includes != null && includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.Where(filters);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync(long id)
            => await _model.FindAsync(id);
        public async Task<T> FindAsync(int id)
          => await _model.FindAsync(id);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> query)
            => await _model.AnyAsync(query);

        public async Task<ErrorOr<T>> AddAsync(T item)
        {
            try
            {
                await _model.AddAsync(item);
                return item;
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }

        //Update
        public async Task UpdateAsync(T model)
        {
            _context.Update(model); 
               // _context.Attach(model);
                //_context.Entry(model).State = EntityState.Modified;          
        }

        //Delete 
        public async Task DeleteAsync(T model)
        {
            if (_context.Entry(model).State is EntityState.Detached)
                _context.Attach(model);

            _model.Remove(model);
        }


        // Transaction

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            IDbContextTransaction dbContextTransaction = await _context.Database.BeginTransactionAsync();
            return dbContextTransaction;
        }
        public async Task RollbackTransactionAsync()
          => await _context.Database.RollbackTransactionAsync();
        public async Task CommitTransactionAsync()
          => await _context.Database.CommitTransactionAsync();


    }

}
