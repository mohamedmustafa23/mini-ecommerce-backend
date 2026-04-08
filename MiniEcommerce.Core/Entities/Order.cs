namespace MiniEcommerce.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; } = DateTime.UtcNow;
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalTotal { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
