using System.Collections.Generic;
using CirqusDemo.Events;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;

namespace CirqusDemo.Views
{
    public class OrdersView : IViewInstance<InstancePerAggregateRootLocator>, ISubscribeTo<ItemAdded>, ISubscribeTo<ItemRemoved>
    {
        public OrdersView()
        {
            ItemCounts = new Dictionary<string, int>();
        }
        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }
        public Dictionary<string, int> ItemCounts { get; set; }

        public void Handle(IViewContext context, ItemAdded domainEvent)
        {
            if (!ItemCounts.ContainsKey(domainEvent.ItemName))
            {
                ItemCounts[domainEvent.ItemName] = 0;
            }

            ItemCounts[domainEvent.ItemName] += domainEvent.Quantity;
        }

        public void Handle(IViewContext context, ItemRemoved domainEvent)
        {
            ItemCounts[domainEvent.ItemName] -= domainEvent.Quantity;
        }
    }
}