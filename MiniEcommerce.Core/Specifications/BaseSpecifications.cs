using MiniEcommerce.Core.Contracts;
using System.Linq.Expressions;

namespace MiniEcommerce.Core.Specifications
{
    public class BaseSpecifications<TEnitiy, Tkey> : ISpecifications<TEnitiy, Tkey> where TEnitiy : class
    {
        public Expression<Func<TEnitiy, bool>> Criteria { get; }

        public BaseSpecifications(Expression<Func<TEnitiy, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        #region Includes
        public ICollection<Expression<Func<TEnitiy, object>>> IncludeExpressions { get; } = [];

        public List<string> IncludeStrings { get; } = [];

        protected void AddInclude(Expression<Func<TEnitiy, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        #endregion 

        #region Sorting
        public Expression<Func<TEnitiy, object>> OrderBy { get; private set; }
        public Expression<Func<TEnitiy, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEnitiy, object>> orderByExperssion)
        {
            OrderBy = orderByExperssion;
        }
        protected void AddOrderByDescending(Expression<Func<TEnitiy, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        #endregion

        #region Pagination
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginationEnabled { get; private set; }

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginationEnabled = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
        #endregion
    }
}