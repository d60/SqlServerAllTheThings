using System;
using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using WebOrder.Messages;
#pragma warning disable 1998

namespace WebOrder.Handlers
{
    public class OrderSaga : Saga<OrderSagaData>, IAmInitiatedBy<PlaceOrder>, IHandleMessages<CancelOrder>, IHandleMessages<FinalizeOrder>
    {
        readonly IBus _bus;

        public OrderSaga(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderSagaData> config)
        {
            config.Correlate<PlaceOrder>(m => m.OrderId, d => d.OrderId);
            config.Correlate<CancelOrder>(m => m.OrderId, d => d.OrderId);
            config.Correlate<FinalizeOrder>(m => m.OrderId, d => d.OrderId);
        }

        public async Task Handle(PlaceOrder message)
        {
            Data.OrderId = message.OrderId;
            Data.Product = message.Product;

            await _bus.Defer(TimeSpan.FromSeconds(10), new FinalizeOrder {OrderId = Data.OrderId});

            Console.WriteLine("Order {0} placed", Data.OrderId);
        }

        public async Task Handle(CancelOrder message)
        {
            Console.WriteLine("Order {0} cancelled!", Data.OrderId);
            MarkAsComplete();
        }

        public async Task Handle(FinalizeOrder message)
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