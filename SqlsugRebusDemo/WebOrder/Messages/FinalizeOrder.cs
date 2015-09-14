using System;

namespace WebOrder.Messages
{
    public class FinalizeOrder
    {
        public Guid OrderId { get; set; } 
    }
}