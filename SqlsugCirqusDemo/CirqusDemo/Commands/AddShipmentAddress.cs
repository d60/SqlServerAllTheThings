using CirqusDemo.Roots;
using CirqusDemo.Values;
using d60.Cirqus.Commands;

namespace CirqusDemo.Commands
{
    public class AddShipmentAddress : Command<Order>
    {
        public AddShipmentAddress(string orderId, Address address)
            : base(orderId)
        {
            Address = address;
        }

        public Address Address { get; private set; }

        public override void Execute(Order aggregateRoot)
        {
            aggregateRoot.AddShipmentAddress(Address);
        }
    }
}