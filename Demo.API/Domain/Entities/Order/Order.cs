using Demo.API.Domain.Entities.Auth;
using Demo.API.Domain.Entities.Catalog;

namespace Demo.API.Domain.Entities.Order
{
    public class Order : Entity
    {
        public User User { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class OrderItem : Entity
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
