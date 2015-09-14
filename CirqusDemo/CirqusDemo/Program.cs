using System;
using CirqusDemo.Commands;
using CirqusDemo.Values;
using CirqusDemo.Views;
using d60.Cirqus;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.MsSql.Config;

namespace CirqusDemo
{
    class Program
    {
        const string MongoConnectionString = "mongodb://localhost/sqlsatt";
        const string SqlConnectionString = "server=.\\SQLEXPRESS; database=sqlsatt; trusted_connection=true";

        static void Main()
        {
            var ordersView = new MongoDbViewManager<OrdersView>(MongoConnectionString);
            var orderedItemsView = new MongoDbViewManager<OrderedItemsView>(MongoConnectionString);
            var regrettableItemsView = new MongoDbViewManager<RegrettableItemsView>(MongoConnectionString);

            var commandProcessor = CommandProcessor.With()
                .Logging(l => l.UseConsole())
                .EventStore(e => e.UseSqlServer(SqlConnectionString, "Events"))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(ordersView, orderedItemsView, regrettableItemsView))
                .Create();

            using (commandProcessor)
            {
                const string orderId = "order/5";

                commandProcessor.ProcessCommand(new CreateNewOrder(orderId));
                commandProcessor.ProcessCommand(new AddItem(orderId, "Beer", 6));
                commandProcessor.ProcessCommand(new AddItem(orderId, "Nuts", 3));
                commandProcessor.ProcessCommand(new RemoveItem(orderId, "Beer", 6));
                commandProcessor.ProcessCommand(new AddShipmentAddress(orderId, new Address("Torsmark", "4", "8700", "Horsens")));

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
    }
}
