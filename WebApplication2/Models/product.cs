using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class product
    {
        public int id {  get; set; }
        public string name { get; set; }    
        public decimal unitprice { get; set; }
        public  int unitinstock {  get; set; }
        [ForeignKey("category")]
        public int categoryid { get; set; }
        public category? category { get; set; }
        public string pic {  get; set; }

        [ForeignKey("supplier")]
        public int supplierid { get; set; } 
        public supplier? supplier { get; set; }

    }
}
