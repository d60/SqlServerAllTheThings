using System.Collections.Generic;

namespace HybridTjek.Model
{
    public class Order
    {
        public string Id { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public Address DeliveryAddress { get; set; }
        public override string ToString()
        {
            return $"Order {Id}: {string.Join(", ", OrderLines)} => {DeliveryAddress}";
        }
    }
}