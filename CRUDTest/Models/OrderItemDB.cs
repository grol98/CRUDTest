using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRUDTest.Models
{
    public class OrderItemDB
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        [Precision(18, 3)]
        public decimal Quantity { get; set; } 

        [Required]
        public string Unit { get; set; }  
    }
}
