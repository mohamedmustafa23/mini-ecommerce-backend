using MiniEcommerce.Core.DTOs;
using MiniEcommerce.Core.Shared;

namespace MiniEcommerce.Core.Services.ServicesAbstraction
{
    public interface IProductService
    {
        Task<PaginationResult<ProductToReturnDto>> GetProductsAsync(int pageIndex, int pageSize);
        Task<ProductToReturnDto> CreateProductAsync(ProductCreateDto productDto);
    }
}
