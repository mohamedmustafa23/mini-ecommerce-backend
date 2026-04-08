using MiniEcommerce.Core.Entities;

namespace MiniEcommerce.Core.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(int pageSize = 10, int pageIndex = 1) : base(null!)
        {
            ApplyPagination(pageSize, pageIndex);
            AddOrderBy(p => p.Name); 
        }

        public ProductSpecifications(int id) : base(p => p.Id == id) { }
    }
}