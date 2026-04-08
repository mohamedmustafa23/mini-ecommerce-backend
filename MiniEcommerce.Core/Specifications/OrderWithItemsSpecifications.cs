using MiniEcommerce.Core.Entities;

namespace MiniEcommerce.Core.Specifications
{
    public class OrderWithItemsSpecifications : BaseSpecifications<Order, int>
    {
        public OrderWithItemsSpecifications(int id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude("OrderItems.Product");
        }
    }
}