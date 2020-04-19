using System;
using Marten.Events.Projections;
using Orders.Api.Domain.Events;

namespace Orders.Api.Domain.Views
{
    public class OrderViewProjection : ViewProjection<OrderView, Guid>
    {
        public OrderViewProjection()
        {
        }
    }
}