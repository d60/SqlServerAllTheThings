using System;
using Rebus;
using WebOrder.Messages;

namespace WebOrder.Handlers
{
    public class OrderSaga : Saga<OrderSagaData>, IAmInitiatedBy<PlaceOrder>, IHandleMessages<CancelOrder>, IHandleMessages<FinalizeOrder>
    {
        readonly IBus _bus;

        public OrderSaga(IBus bus)
        {
            _bus = bus;
        }

        public override void ConfigureHowToFindSaga()
        {
            Incoming<CancelOrder>(p => p.OrderId).CorrelatesWith(d => d.OrderId);
            Incoming<FinalizeOrder>(p => p.OrderId).CorrelatesWith(d => d.OrderId);
        }

        public void Handle(PlaceOrder message)
        {
            Data.OrderId = message.OrderId;
            Data.Product = message.Product;

            _bus.Defer(TimeSpan.FromSeconds(10), new FinalizeOrder {OrderId = Data.OrderId});

            Console.WriteLine("Order {0} placed", Data.OrderId);
        }

        public void Handle(CancelOrder message)
        {
            Console.WriteLine("Order {0} cancelled!", Data.OrderId);
            MarkAsComplete();
        }

        public void Handle(FinalizeOrder message)
        {
            Console.WriteLine("Finalizing order {0} - there's no way back now!", Data.OrderId);
            MarkAsComplete();
        }
    }

    public class OrderSagaData : ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }
        
        public string Product { get; set; }
        public Guid OrderId { get; set; }
    }
}