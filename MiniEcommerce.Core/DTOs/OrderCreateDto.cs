using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerce.Core.DTOs
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string CustomerName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string CustomerEmail { get; set; } = string.Empty;
        public List<OrderItemCreateDto> Items { get; set; } = new();
    }
}
