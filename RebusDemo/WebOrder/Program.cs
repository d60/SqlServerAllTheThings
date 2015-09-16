using System;
using System.Threading;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Persistence.SqlServer;
using Rebus.Transport.SqlServer;
using WebOrder.Handlers;
using WebOrder.Messages;

namespace WebOrder
{
    class Program
    {
        const string Conn = "server=.\\SQLEXPRESS; database=sqlsatt; trusted_connection=true";

        static void Main()
        {
            using (var adapter = new BuiltinHandlerActivator())
            {
                adapter.Register((bus, context) => new OrderSaga(bus));

                Configure.With(adapter)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                    .Transport(t => t.UseSqlServer(Conn, "RebusMessages", "OrderQueue"))
                    .Sagas(s => s.StoreInSqlServer(Conn, "OrderSagas", "OrderSagasIndex"))
                    .Start();

                while (true)
                {
                    Console.WriteLine("Input order");
                    Console.Write("> ");

                    var text = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(text)) break;

                    var orderId = Guid.NewGuid();

                    adapter.Bus.SendLocal(new PlaceOrder
                    {
                        OrderId = orderId,
                        Product = text
                    }).Wait();

                    Thread.Sleep(3000);

                    Console.WriteLine("Press C to cancel the order");
                    var key = char.ToLower(Console.ReadKey().KeyChar);

                    if (key != 'c') continue;

                    Console.WriteLine("Cancelling...");

                    adapter.Bus.SendLocal(new CancelOrder
                    {
                        OrderId = orderId
                    }).Wait();

                    Thread.Sleep(3000);
                }
            }
        }
    }
}
