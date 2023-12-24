namespace DataBase.Models
{
    public class Category : ModelBase
    {
        public string? Name { get; set; }

        public ICollection<Product> Items { get; set; }
    }
}
