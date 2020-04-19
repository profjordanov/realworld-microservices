using System.Collections.Generic;
using Orders.Api.Domain.Entities;
using Orders.Api.Domain.Events._Base;

namespace Orders.Api.Domain.Events
{
    public class OrderDetailsSubmitted : IEvent
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}