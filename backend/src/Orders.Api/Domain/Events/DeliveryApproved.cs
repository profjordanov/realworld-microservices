using Orders.Api.Domain.Entities;
using Orders.Api.Domain.Events._Base;

namespace Orders.Api.Domain.Events
{
    public class DeliveryApproved : IEvent
    {
        public string UserId { get; set; }
        public Delivery Delivery { get; set; }
        public Order Order { get; set; }
    }
}