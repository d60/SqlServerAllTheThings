using System;
using System.Threading;
using Rebus.Configuration;
using Rebus.Logging;
using Rebus.Transports.Sql;
using WebOrder.Handlers;
using WebOrder.Messages;

namespace WebOrder
{
    class Program
    {
        const string Conn = "server=.;database=sqlsug;trusted_connection=true";

        static void Main()
        {
            using (var adapter = new BuiltinContainerAdapter())
            {
                adapter.Register(() => new OrderSaga(adapter.Bus));

                var bus = Configure.With(adapter)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                    .Transport(t => t.UseSqlServer(Conn, "RebusMessages", "OrderQueue", "Error").EnsureTableIsCreated())
                    .Sagas(s => s.StoreInSqlServer(Conn, "OrderSagas", "OrderSagasIndex").EnsureTablesAreCreated())
                    .Timeouts(t => t.StoreInSqlServer(Conn, "RebusTimeouts").EnsureTableIsCreated())
                    .CreateBus()
                    .Start();

                while (true)
                {
                    Console.WriteLine("Input order");
                    Console.Write("> ");

                    var text = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(text)) break;

                    var orderId = Guid.NewGuid();

                    bus.SendLocal(new PlaceOrder
                    {
                        OrderId =  orderId,
                        Product = text
                    });

                    Thread.Sleep(3000);

                    Console.WriteLine("Press C to cancel the order");
                    var key = char.ToLower(Console.ReadKey().KeyChar);

                    if (key != 'c') continue;

                    Console.WriteLine("Cancelling...");

                    bus.SendLocal(new CancelOrder
                    {
                        OrderId = orderId
                    });

                    Thread.Sleep(3000);
                }
            }
        }
    }
}
