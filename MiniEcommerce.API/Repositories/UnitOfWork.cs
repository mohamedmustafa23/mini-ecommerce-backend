using MiniEcommerce.API.Data;
using MiniEcommerce.Core.Contracts;

namespace MiniEcommerce.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            var EntityType = typeof(TEntity);

            if(_repositories.TryGetValue(EntityType, out var repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }

            var newRepository = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = newRepository;
            return newRepository;
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
