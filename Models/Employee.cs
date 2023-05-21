using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    public class Employee
    {
        [Required ]
        [Key]
        public int employeeID { get; set; } 
        public string name { get; set; }

        public DateTime dateofbirth { get; set; }

        string email { get; set; }
        string pw { get; set; }

        [ForeignKey("Farmer")]
        int farmerID { get; set; }   
      
    }
}
