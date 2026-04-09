using MiniEcommerce.Core.DTOs;

namespace MiniEcommerce.Core.ServicesAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> PlaceOrderAsync(OrderCreateDto orderDto);
        Task<OrderToReturnDto?> GetOrderByIdAsync(int id);
    }
}
