using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDTest.Models
{
    public class OrderDB
    {
        [Required]
        public int Id { get; set; }

        [Required]     
        public string Number { get; set; }    // Номер заказа

        [Required]
        [Precision(7)]
        public DateTime Date { get; set; }    // Дата забора заказа

        [Required]
        public int ProviderId { get; set; }   // Id поставщика
    }
}
