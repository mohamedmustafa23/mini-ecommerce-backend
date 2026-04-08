using MiniEcommerce.Core.DTOs;

namespace MiniEcommerce.Core.Services.ServicesAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> PlaceOrderAsync(OrderCreateDto orderDto);
        Task<OrderToReturnDto?> GetOrderByIdAsync(int id);
    }
}
