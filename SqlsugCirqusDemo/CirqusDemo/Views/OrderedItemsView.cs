using System.Collections.Generic;
using CirqusDemo.Events;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;

namespace CirqusDemo.Views
{
    public class OrderedItemsView : IViewInstance<OrderedItemsView.InstancePerItemLocator>, ISubscribeTo<ItemAdded>, ISubscribeTo<ItemRemoved>
    {
        public string Id { get; set; }

        public long LastGlobalSequenceNumber { get; set; }

        public int TotalQuantity { get; set; }

        public class InstancePerItemLocator : HandlerViewLocator, IGetViewIdsFor<ItemAdded>, IGetViewIdsFor<ItemRemoved>
        {
            public IEnumerable<string> GetViewIds(IViewContext context, ItemAdded e)
            {
                yield return e.ItemName;
            }

            public IEnumerable<string> GetViewIds(IViewContext context, ItemRemoved e)
            {
                yield return e.ItemName;
            }
        }

        public void Handle(IViewContext context, ItemAdded domainEvent)
        {
            TotalQuantity += domainEvent.Quantity;
        }

        public void Handle(IViewContext context, ItemRemoved domainEvent)
        {
            TotalQuantity -= domainEvent.Quantity;
        }
    }
}