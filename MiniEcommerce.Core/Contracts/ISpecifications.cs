using System.Linq.Expressions;

namespace MiniEcommerce.Core.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : class
    {
        // Filtering
        Expression<Func<TEntity, bool>> Criteria { get; }

        // Includes (Expressions)
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        List<string> IncludeStrings { get; }

        // Sorting
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }

        // Pagination
        int Take { get; }
        int Skip { get; }
        bool IsPaginationEnabled { get; }
    }
}