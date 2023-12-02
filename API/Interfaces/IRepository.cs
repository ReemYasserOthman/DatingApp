using System.Linq.Expressions;
using API.Entities;

namespace  API.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        IEnumerable<TEntity> AddRange(List<TEntity> entity);
        void Update(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetAllByAsQueryable(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetByIdAsync(int id);
        TEntity GetBy(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression);


        Task<TEntity> GetFirstOrderByAsync(Expression<Func<TEntity, object>> expression);
        Task<TEntity> GetLastOrderByAsync(Expression<Func<TEntity, object>> expression);



        // Task<TEntity> GetEntityWithSpec (ISpecification<TEntity> spec);
        // Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);


        Task<IEnumerable<T>> Map_GetAllAsync<T>();
        Task<IEnumerable<T>> Map_GetAllByAsync<T>(Expression<Func<TEntity, bool>> expression);
        // Task<IEnumerable<T>> Map_GetAllByAsyncX<T>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderBy = null);
        Task<T> Map_GetByAsync<T>(Expression<Func<TEntity, bool>> expression);

        Task<T> Map_GetByIdAsync<T>(int id);

        IQueryable<T> Map_GetAllByAsQueryable<T>(Expression<Func<TEntity, bool>> expression);


        //  Task<PagedList<T>> GetAllQueryableBy<T>(Expression<Func<TEntity, bool>> expression
        //     , Expression<Func<TEntity,object>> orderBy
        //     , PaginationParams paginationParams) where T : class;
        Task<decimal> GetAllQueryableSumCol(Expression<Func<TEntity, bool>> expression
            , Expression<Func<TEntity, decimal>> sum);

        Task<IQueryable<IGrouping<DateTime, TEntity>>> GetAllQueryableGrouping(Expression<Func<TEntity, bool>> expression
        , Expression<Func<TEntity, DateTime>> group);
    }
}