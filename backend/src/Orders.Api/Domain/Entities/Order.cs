using System;
using System.Collections.Generic;
using System.Linq;
using Orders.Api.Domain.Events;

namespace Orders.Api.Domain.Entities
{
    public class Order 
    {
        public string Id { get; set; }

        public bool IsNew { get; set; }

        public bool IsPending { get; set; }

        public bool IsBooked { get; set; }

        public string UserEmail { get; set; }

        public string UserFullName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public decimal DeliveryTax { get; set; }

        public decimal TotalAmount => OrderItems.Sum(item => item.Price) + DeliveryTax;

        //public OrderDetailsSubmitted SubmitDetails() => new OrderDetailsSubmitted
        //{
        //    Email = UserEmail,
        //    FullName = UserFullName,
        //    Phone = Phone,
        //    City = City,
        //    Address = Address,
        //    OrderItems = OrderItems
        //};
    }
}