namespace DataBase.Models
{
    public class Product : ModelBase
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public string Number { get; set; }
    }
}
