using CirqusDemo.Roots;
using d60.Cirqus.Commands;

namespace CirqusDemo.Commands
{
    public class CreateNewOrder : Command<Order>
    {
        public CreateNewOrder(string orderId) : base(orderId)
        {
        }

        public override void Execute(Order aggregateRoot)
        {
            aggregateRoot.Create();
        }
    }
}