using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRUDTest.Models
{
    public class ProviderDB
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    
    }
}
