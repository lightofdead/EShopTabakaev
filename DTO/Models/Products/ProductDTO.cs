namespace DTO.Models.Products
{
    public class ProductDTO : DTOModelBase
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public Guid CategoryId { get; set; }
    }
}
