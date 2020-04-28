using System;
using Orders.Api.Domain.Events;

namespace Orders.Api.Domain.Entities
{
    public class DeliveryApproval
    {
        public Guid Id { get; set; }

        public ApprovalStatus Status { get; set; }

        public DateTimeOffset Date { get; set; }

        public string UserId { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string DeliveryId { get; set; }

        public Delivery Delivery { get; set; }

        public string Notes { get; set; }

        public DeliveryApproved Approve() => new DeliveryApproved
        {
            Order = Order,
            Delivery = Delivery,
            UserId = UserId
        };

        public void Apply(DeliveryApproved @event)
        {
            Id = Guid.NewGuid();
            UserId = @event.UserId;
            OrderId = @event.Order?.Id;
            Order = @event.Order;
            Delivery = @event.Delivery;
            Status = ApprovalStatus.Approved;
        }

        public DeliveryRejected Reject() => new DeliveryRejected
        {
            Order = Order,
            Delivery = Delivery,
            Note = Notes,
            UserId = UserId
        };

        public void Apply(DeliveryRejected @event)
        {
            Id = Guid.NewGuid();
            UserId = @event.UserId;
            OrderId = @event.Order?.Id;
            Order = @event.Order;
            Delivery = @event.Delivery;
            Status = ApprovalStatus.Rejected;
        }
    }

    public enum ApprovalStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 4
    }
}