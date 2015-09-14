using CirqusDemo.Roots;
using d60.Cirqus.Commands;

namespace CirqusDemo.Commands
{
    public class RemoveItem : Command<Order>
    {
        public RemoveItem(string orderId, string itemName, int quantity)
            : base(orderId)
        {
            ItemName = itemName;
            Quantity = quantity;
        }

        public string ItemName { get; private set; }

        public int Quantity { get; private set; }

        public override void Execute(Order aggregateRoot)
        {
            aggregateRoot.RemoveItem(ItemName, Quantity);
        }
    }
}