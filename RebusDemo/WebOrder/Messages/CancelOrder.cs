using System;

namespace WebOrder.Messages
{
    public class CancelOrder
    {
        public Guid OrderId { get; set; }
    }
}