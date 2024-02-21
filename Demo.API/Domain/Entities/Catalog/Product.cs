namespace Demo.API.Domain.Entities.Catalog
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
