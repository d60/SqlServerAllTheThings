using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using HybridDb;

namespace HybridTjek
{
    class Program
    {
        const string ConnectionString = "server=.;database=sqlsug;trusted_connection=true";

        static void Main()
        {
            var documentStore = new DocumentStore(ConnectionString);

            documentStore.Configuration.UseSerializer(new DefaultJsonSerializer());

            documentStore.Document<Order>()
                .Project(d => d.DeliveryAddress.HouseNumber)
                .Project(d => d.DeliveryAddress.PostalCode)
                .Project(d => d.DeliveryAddress.City);

            documentStore.MigrateSchemaToMatchConfiguration();

            CreateSomeOrders(documentStore);

            QuerySomeOrders(documentStore);
        }

        static void QuerySomeOrders(DocumentStore documentStore)
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

        static void CreateSomeOrders(DocumentStore documentStore)
        {
            using (var session = documentStore.OpenSession())
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine {ItemName = "beer", Quantity = 6},
                        new OrderLine {ItemName = "nuts", Quantity = 2},
                        new OrderLine {ItemName = "big tv", Quantity = 1},
                    },
                    DeliveryAddress = new Address
                    {
                        Street = "Torsmark",
                        HouseNumber = "6",
                        PostalCode = "8700",
                        City = "Horsens"
                    }
                };

                session.Store(order);

                session.SaveChanges();
            }
        }
    }

    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public Address DeliveryAddress { get; set; }
        public override string ToString()
        {
            return string.Format("Order {0}: {1} => {2}", Id, string.Join(", ", OrderLines), DeliveryAddress);
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}, {2} {3}", Street, HouseNumber, PostalCode, City);
        }
    }

    public class OrderLine
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public override string ToString()
        {
            return string.Format("{0} x {1}", Quantity, ItemName);
        }
    }
}
