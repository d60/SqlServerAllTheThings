using System;
using System.Linq;
using HybridTjek.Model;
using HybridTjek.Util;

namespace HybridTjek
{
    class Program
    {
        const string ConnectionString = "server=.\\SQLEXPRESS; database=sqlsatt; trusted_connection=true";

        static void Main()
        {
            var orderId = Guid.NewGuid();
            var order = CreateOrder(orderId.ToString());





        }

        static Order CreateOrder(string id)
        {
            Console.WriteLine("Creating order with ID {0}", id);

            return new Order
            {
                Id = id,

                OrderLines = Enumerable.Range(0, Random.Next(5) + 1)
                    .Select(i => new OrderLine
                    {
                        ItemName = Random.ItemFrom(ItemNames),
                        Quantity = Random.Next(5) + 1
                    })
                    .ToList(),

                DeliveryAddress = Random.ItemFrom(DeliveryAddresses)
            };
        }

        static readonly string[] ItemNames = { "Beer", "Nuts", "Big TV", "Burger", "Pizza" };

        static readonly Address[] DeliveryAddresses =
        {
            new Address {Street = "Torsmark", HouseNumber = "4", PostalCode = "8700", City = "Horsens"},
            new Address {Street = "Sdr. Ringgade", HouseNumber = "53", PostalCode = "8000", City = "Aarhus"},
            new Address {Street = "Ryesgade", HouseNumber = "5", PostalCode = "2200", City = "Nørrebronx"},
            new Address {Street = "Spobjergvej", HouseNumber = "52", PostalCode = "8220", City = "Brabrand"}
        };

        static readonly Random Random = new Random(DateTime.Now.GetHashCode());
    }
}
