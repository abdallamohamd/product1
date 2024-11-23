namespace WebApplication2.Models
{
    public class supplier
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string country {  get; set; }
        public ICollection<product>? products { get; set; }
    }
}
