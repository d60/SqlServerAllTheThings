using System;
using System.Linq;
using HybridDb;
using HybridTjek.Model;
using HybridTjek.Util;

namespace HybridTjek
{
    class Program
    {
        const string ConnectionString = "server=.\\SQLEXPRESS; database=sqlsatt; trusted_connection=true";

        static void Main()
        {
            var configurator = new LambdaHybridDbConfigurator(c => { });

            using (var documentStore = DocumentStore.Create(ConnectionString, configurator))
            {
                //Random.Next(10).Times(() =>
                //{
                //    CreateOrder(documentStore);
                //});

                //QuerySomeOrders(documentStore);

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }

        static void QuerySomeOrders(IDocumentStore documentStore)
        {
            const string postalCodeToLookFor = "8700";

            using (var session = documentStore.OpenSession())
            {
                var orders = from order in session.Query<Order>()
                             where order.DeliveryAddress.PostalCode == postalCodeToLookFor
                             select order;

                Console.WriteLine(@"Found the following orders for {0}:
{1}", postalCodeToLookFor, string.Join(Environment.NewLine, orders));
            }
        }

        static void CreateOrder(IDocumentStore documentStore)
        {
            var id = Guid.NewGuid().ToString();

            Console.WriteLine("Creating order with ID {0}", id);

            using (var session = documentStore.OpenSession())
            {
                var order = new Order
                {
                    Id = id,

                    OrderLines = Enumerable.Range(0, Random.Next(3) + 1)
                        .Select(i => new OrderLine
                        {
                            ItemName = Random.ItemFrom(ItemNames),
                            Quantity = Random.Next(5) + 1
                        })
                        .ToList(),

                    DeliveryAddress = Random.ItemFrom(DeliveryAddresses)
                };

                session.Store(order);

                session.SaveChanges();
            }
        }

        static readonly string[] ItemNames = { "Beer", "Nuts", "Big TV", "Burger", "Pizza" };

        static readonly Address[] DeliveryAddresses =
        {
            new Address
            {
                Street = "Torsmark",
                HouseNumber = "4",
                PostalCode = "8700",
                City = "Horsens"
            },
            new Address
            {
                Street = "Sdr. Ringgade",
                HouseNumber = "53",
                PostalCode = "8000",
                City = "Aarhus"
            },
            new Address
            {
                Street = "Ryesgade",
                HouseNumber = "5",
                PostalCode = "2200",
                City = "Nørrebronx"
            },
            new Address
            {
                Street = "Spobjergvej",
                HouseNumber = "52",
                PostalCode = "8220",
                City = "Brabrand"
            }
        };

        static readonly Random Random = new Random(DateTime.Now.GetHashCode());
    }
}
