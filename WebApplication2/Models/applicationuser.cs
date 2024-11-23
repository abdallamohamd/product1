using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models
{
    public class applicationuser :IdentityUser
    {
        public string address {  get; set; }
    }
}
