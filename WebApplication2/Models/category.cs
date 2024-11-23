namespace WebApplication2.Models
{
    public class category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<product>? products { get; set; }
    }
}
