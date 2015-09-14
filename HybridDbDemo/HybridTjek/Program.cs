using System;
using System.Linq;
using HybridDb;
using HybridTjek.Model;

namespace HybridTjek
{
    class Program
    {
        const string ConnectionString = "server=.\\SQLEXPRESS; database=sqlsatt; trusted_connection=true";

        static void Main()
        {
            var configurator = new LambdaHybridDbConfigurator(c =>
            {
                c.Document<Order>();
            });

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
            using (var session = documentStore.OpenSession())
            {
                var order = new Order
                {
                    Id = Guid.NewGuid().ToString(),

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
                Street = "Tuborg Boulevard",
                HouseNumber = "12",
                PostalCode = "2900",
                City = "Hellerup"
            },
            new Address
            {
                Street = "Ryesgade",
                HouseNumber = "5",
                PostalCode = "2200",
                City = "Nørrebronx"
            }
        };

        static readonly Random Random = new Random(DateTime.Now.GetHashCode());
    }
}
