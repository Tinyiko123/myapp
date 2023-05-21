using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    public class Farmer
    {
        public int farmerID { get; set; }
        public string fname { get; set; }

        public string lname { get; set; }

        public string email { get; set; }   

        public string pw { get; set; }


        [ForeignKey("Product")]
        public int productCode { get; set; }    

      
    }
}
