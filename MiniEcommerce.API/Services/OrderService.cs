using MiniEcommerce.Core.Contracts;
using MiniEcommerce.Core.DTOs;
using MiniEcommerce.Core.Entities;
using MiniEcommerce.Core.ServicesAbstraction;
using MiniEcommerce.Core.Specifications;

namespace MiniEcommerce.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        public OrderService(IUnitOfWork uow) => _uow = uow;

        public async Task<OrderToReturnDto> PlaceOrderAsync(OrderCreateDto orderDto)
        {
            var orderItems = new List<OrderItem>();
            decimal subtotal = 0;
            int totalItemsCount = 0;

            foreach (var itemDto in orderDto.Items)
            {
                var product = await _uow.GetRepository<Product, int>().GetByIdAsync(new BaseSpecifications<Product, int>(p => p.Id == itemDto.ProductId));

                if (product == null || product.AvailableQuantity < itemDto.Quantity)
                    throw new Exception($"Stock insufficient for product {product?.Name}");

                product.AvailableQuantity -= itemDto.Quantity;

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                };
                orderItems.Add(orderItem);
                subtotal += orderItem.UnitPrice * orderItem.Quantity;
                totalItemsCount += orderItem.Quantity;
            }

            decimal discount = totalItemsCount >= 5 ? 0.10m : (totalItemsCount >= 2 ? 0.05m : 0);

            var order = new Order
            {
                CustomerName = orderDto.CustomerName,
                CustomerEmail = orderDto.CustomerEmail,
                OrderItems = orderItems,
                SubTotal = subtotal,
                DiscountAmount = subtotal * discount,
                FinalTotal = subtotal - (subtotal * discount)
            };

            await _uow.GetRepository<Order, int>().AddAsync(order);
            await _uow.SaveChangesAsync();

            return MapToDto(order);
        }

        public async Task<OrderToReturnDto?> GetOrderByIdAsync(int id)
        {
            var spec = new OrderWithItemsSpecifications(id);
            var order = await _uow.GetRepository<Order, int>().GetByIdAsync(spec);
            return order == null ? null : MapToDto(order);
        }

        private OrderToReturnDto MapToDto(Order order) => new OrderToReturnDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CreatAt = order.CreatAt,
            SubTotal = order.SubTotal,
            DiscountAmount = order.DiscountAmount,
            FinalTotal = order.FinalTotal,

            OrderItems = order.OrderItems.Select(i => new OrderItemDto
            {
                ProductName = i.Product?.Name ?? "Product Not Found",
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
    }
}
