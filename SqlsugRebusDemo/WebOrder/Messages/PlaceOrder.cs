using System;

namespace WebOrder.Messages
{
    public class PlaceOrder
    {
        public Guid OrderId { get; set; }
        public string Product { get; set; } 
    }
}