using System.ComponentModel.DataAnnotations;

namespace WebApplication2.view_model
{
    public class regestervm
    {
        public string name { get; set; }    
        public string address { get; set; }
        public string phone {  get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
