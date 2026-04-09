using MiniEcommerce.Core.Contracts;
using MiniEcommerce.Core.DTOs;
using MiniEcommerce.Core.Entities;
using MiniEcommerce.Core.ServicesAbstraction;
using MiniEcommerce.Core.Shared;
using MiniEcommerce.Core.Specifications;

namespace MiniEcommerce.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        public ProductService(IUnitOfWork uow) => _uow = uow;

        public async Task<PaginationResult<ProductToReturnDto>> GetProductsAsync(int pageIndex, int pageSize)
        {
            var spec = new ProductSpecifications(pageSize, pageIndex);
            var products = await _uow.GetRepository<Product, int>().GetAllAsync(spec);
            var totalCount = await _uow.GetRepository<Product, int>().CountAsync(new BaseSpecifications<Product, int>(null!));

            var data = products.Select(p => new ProductToReturnDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                AvailableQuantity = p.AvailableQuantity
            });

            return new PaginationResult<ProductToReturnDto>(pageIndex, pageSize, totalCount, data);
        }

        public async Task<ProductToReturnDto> CreateProductAsync(ProductCreateDto dto)
        {
            var product = new Product { Name = dto.Name, Price = dto.Price, AvailableQuantity = dto.AvailableQuantity };
            await _uow.GetRepository<Product, int>().AddAsync(product);
            await _uow.SaveChangesAsync();

            return new ProductToReturnDto { Id = product.Id, Name = product.Name, Price = product.Price , AvailableQuantity = product.AvailableQuantity };
        }
    }
}
