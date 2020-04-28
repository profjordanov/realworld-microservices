using Orders.Api.Domain.Entities;

namespace Orders.Api.Domain.Events
{
    public class DeliveryRejected
    {
        public string UserId { get; set; }

        public Delivery Delivery { get; set; }
        public Order Order { get; set; }

        public string Note { get; set; }
    }
}