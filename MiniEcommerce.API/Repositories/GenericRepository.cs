using Microsoft.EntityFrameworkCore;
using MiniEcommerce.API.Data;
using MiniEcommerce.Core.Contracts;

namespace MiniEcommerce.API.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _db; 

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _db = _dbContext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity) => await _db.AddAsync(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync()=> await _db.ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_db, specifications).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_db, specifications).FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity) => _db.Remove(entity);

        public void Update(TEntity entity) => _db.Update(entity);
        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_db, specifications).CountAsync();
        }

    }
}
