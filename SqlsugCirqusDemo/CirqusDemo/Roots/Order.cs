using CirqusDemo.Events;
using CirqusDemo.Values;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Events;

namespace CirqusDemo.Roots
{
    public class Order : AggregateRoot, IEmit<OrderCreated>, IEmit<ItemAdded>, IEmit<ItemRemoved>, IEmit<ShipmentAddressAdded>
    {
        public void Apply(OrderCreated e)
        {
        }

        public void Apply(ItemAdded e)
        {
        }

        public void Apply(ItemRemoved e)
        {
        }

        public void Apply(ShipmentAddressAdded e)
        {
        }

        public void Create()
        {
            Emit(new OrderCreated());
        }

        public void AddItem(string itemName, int quantity)
        {
            Emit(new ItemAdded(itemName, quantity));
        }

        public void RemoveItem(string itemName, int quantity)
        {
            Emit(new ItemRemoved(itemName, quantity));
        }

        public void AddShipmentAddress(Address address)
        {
            Emit(new ShipmentAddressAdded(address));
        }
    }
}