using CirqusDemo.Roots;
using CirqusDemo.Values;
using d60.Cirqus.Events;

namespace CirqusDemo.Events
{
    public class ShipmentAddressAdded : DomainEvent<Order>
    {
        public ShipmentAddressAdded(Address address)
        {
            Address = address;
        }

        public Address Address { get; private set; }
    }
}