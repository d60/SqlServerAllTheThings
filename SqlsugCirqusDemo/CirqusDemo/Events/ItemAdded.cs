using CirqusDemo.Roots;
using d60.Cirqus.Events;

namespace CirqusDemo.Events
{
    public class ItemAdded : DomainEvent<Order>
    {
        public ItemAdded(string itemName, int quantity)
        {
            ItemName = itemName;
            Quantity = quantity;
        }

        public string ItemName { get; private set; }

        public int Quantity { get; private set; }
    }
}