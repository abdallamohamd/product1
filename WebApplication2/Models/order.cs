using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int qantity {  get; set; }
        [ForeignKey("product")]
        public int productid {  get; set; }

        public DateTime orderdate { get; set; }
       public product? product { get; set; }
    }
}
