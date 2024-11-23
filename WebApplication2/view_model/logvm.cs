using System.ComponentModel.DataAnnotations;

namespace WebApplication2.view_model
{
    public class logvm
    { 
        public string name {  get; set; }

        [DataType(DataType.Password)]
        public string password {  get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string confirmpassword {  get; set; }
        public bool remmber { get; set; }
    }
}
