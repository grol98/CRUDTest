using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDTest.Models
{
    public class EditOrderClass
    { 
        public OrderWeb orderWeb { get; set; }               // заказ
        public List<ProviderDB> ProviderList { get; set; }   // список поставщиков
    }
}
