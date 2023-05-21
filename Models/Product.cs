using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    public class Product   
    {
        public int productCode { get; set; }
        public string? productName { get; set; }
        public string? incomingorOutgoing { get; set; }
        public string? type { get; set; }
        public DateTime? dateAcquired { get; set; }
        
    }
}

