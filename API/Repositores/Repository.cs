using System.Linq.Expressions;
using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositores
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Repository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }


        public IEnumerable<TEntity> AddRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);

        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public IQueryable<TEntity> GetAllByAsQueryable(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>()
                .Where(expression)
                .Where(x => x.IsDeleted == false)
                .AsQueryable<TEntity>();
        }

        public TEntity GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefault(expression);
        }
        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }



        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        // private async IQueryable<TEntity> GetByIdQueryable(int id)
        // {
        //     // return SpecificationsEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        //     return await _context.Set<TEntity>().Where(x=> x.Id == id).AsQueryable<TEntity>();
        // }

        public async Task<TEntity> GetFirstOrderByAsync(Expression<Func<TEntity, object>> expression)
        {
            return await _context.Set<TEntity>().OrderBy(expression).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetLastOrderByAsync(Expression<Func<TEntity, object>> expression)
        {
            return await _context.Set<TEntity>().OrderBy(expression).LastOrDefaultAsync();
        }


        // public async Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> spec)
        // {
        //     var x = await ApplySpecification(spec).FirstOrDefaultAsync();

        //     return x;
        // }

        // public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec)
        // {
        //     var x = await ApplySpecification(spec).ToListAsync();

        //     return x;
        // }

        // private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        // {
        //     // return SpecificationsEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        // }

        public async Task<IEnumerable<T>> Map_GetAllAsync<T>()
        {
            return await _context.Set<TEntity>()
                .Where(x => x.IsDeleted == false)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> Map_GetAllByAsync<T>(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>()
                .Where(x => x.IsDeleted == false)
                .Where(expression)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public IQueryable<T> Map_GetAllByAsQueryable<T>(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>()
                .Where(expression)
                .Where(x => x.IsDeleted == false)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .AsQueryable<T>();
        }

        // public async Task<IEnumerable<T>> Map_GetAllByAsyncX<T>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderBy = null)
        // {
        //     return await _context.Set<TEntity>()
        //         .Where(expression)
        //         .OrderByDescending(orderBy)
        //         .ProjectTo<T>(_mapper.ConfigurationProvider)
        //         .ToListAsync();
        // }

        public async Task<T> Map_GetByAsync<T>(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>()
                .Where(expression)
                .Where(x => x.IsDeleted == false)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();


        }

        public async Task<T> Map_GetByIdAsync<T>(int id)
        {
            return await _context.Set<TEntity>()
                .Where(x => x.Id == id)
                .Where(x => x.IsDeleted == false)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();


        }

        // public async Task<PagedList<T>> GetAllQueryableBy<T>(Expression<Func<TEntity, bool>> expression
        // , Expression<Func<TEntity, object>> orderBy
        // , PaginationParams paginationParams) where T : class
        // {
            // var query = _context.Set<TEntity>()
            //     .Where(expression)
            //     .OrderByDescending(orderBy)
            //     .AsQueryable();

            // var queryToCreate = query.ProjectTo<T>(_mapper.ConfigurationProvider).AsNoTracking();

            // var result = await PagedList<T>.CreateAsync(queryToCreate, paginationParams);

            // return result;
        

        // }

        public async Task<decimal> GetAllQueryableSumCol(Expression<Func<TEntity, bool>> expression
            , Expression<Func<TEntity, decimal>> sum)
        {
            var result = _context.Set<TEntity>()
                .Where(expression)
                .AsQueryable()
                .Sum(sum);

            return await Task.Run(() => result);
        }

        public async Task<IQueryable<IGrouping<DateTime, TEntity>>> GetAllQueryableGrouping(Expression<Func<TEntity, bool>> expression
        , Expression<Func<TEntity, DateTime>> group)
        {
            var result = _context.Set<TEntity>()
                .Where(expression)
                .GroupBy(group)
                .AsQueryable();

            // .Sum(sum);

            return await Task.Run(() => result);
        }
    }
}