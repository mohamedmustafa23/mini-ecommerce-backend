using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Core.Contracts;

namespace MiniEcommerce.API
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint, ISpecifications<TEntity, TKey> specifications) where TEntity : class
        {
            var query = EntryPoint;

            if (specifications is not null)
            {
                // 1. Filtering
                if (specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);
                }

                // 2. Expression-based Includes (العادية)
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
                }

                // 3. String-based Includes (التعديل الجديد للـ Nested Includes)
                if (specifications.IncludeStrings is not null && specifications.IncludeStrings.Any())
                {
                    query = specifications.IncludeStrings.Aggregate(query, (currentQuery, includeString) => currentQuery.Include(includeString));
                }

                // 4. Sorting
                if (specifications.OrderBy is not null)
                {
                    query = query.OrderBy(specifications.OrderBy);
                }
                if (specifications.OrderByDescending is not null)
                {
                    query = query.OrderByDescending(specifications.OrderByDescending);
                }

                // 5. Pagination
                if (specifications.IsPaginationEnabled)
                {
                    query = query.Skip(specifications.Skip).Take(specifications.Take);
                }
            }
            return query;
        }
    }
}