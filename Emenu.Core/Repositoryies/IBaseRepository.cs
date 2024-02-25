using Emenu.Core.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.EntityFrameworkCore.Storage;

namespace Emenu.Core.Repositoryies
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<ErrorOr<int>> GetCountAsync(Expression<Func<T, bool>> query = null);
        public Task<List<TResult>> FindListAsync<TResult>(Expression<Func<T, bool>>? match,
            Expression<Func<T, TResult>> selectExpression,
            int? take,
            int? skip,
            Expression<Func<T, Object>> orderBy = null,
            string orderByDirction = OrderBy.Ascending,
            string[] includes = null,
            bool? distinct = null,
            bool asTracking = false);
        public Task<List<T>> FindListAsync(Expression<Func<T, bool>>? match,
           int? take,
           int? skip,
           Expression<Func<T, Object>> orderBy = null,
           string orderByDirction = OrderBy.Ascending,
           string[] includes = null,
           bool? distinct = null,
           bool asTracking = false);

        public Task<ErrorOr<TResult>> FindSingleAsync<TResult>(Expression<Func<T, bool>>? match,
            Expression<Func<T, TResult>> selectExpression,
            string[] includes = null,
            bool asTracking = false);

        public Task<T> FindSingleAsync(Expression<Func<T, bool>>? match,
            string[] includes = null,
            bool asTracking = false);
        public Task<T> FindAsync(long id);
        public Task<T> FindAsync(int id);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> query);

        public Task<ErrorOr<T>> AddAsync(T item);

        public Task UpdateAsync(T model);

        Task DeleteAsync(T model);

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task RollbackTransactionAsync();
        Task CommitTransactionAsync();
    }
}
