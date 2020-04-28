using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Marten.Linq;
using Orders.Api.Domain.Entities;
using Orders.Api.Domain.Events;

namespace Orders.Api.Persistence
{
    public class DeliveryApprovalRepository
    {
        private readonly IDocumentSession _session;

        public DeliveryApprovalRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<string> GetApprovedDeliveries()
        {
            var result = _session
                .Events
                .QueryAllRawEvents()
                .OfType<DeliveryApproved>();

            return await result.ToJsonArrayAsync();
        }

        public async Task<IReadOnlyList<DeliveryApproval>> GetBySomething()
        {
            var approvals = await _session
                .Query<DeliveryApproval>()
                .Where(approval => approval.Order.City == "Varna")
                .ToListAsync();

            return approvals;
        }

        public async Task ApproveByOrderAsync(string userId, Order order)
        {
            var aggregate = new DeliveryApproval
            {
                Order = order,
                UserId = userId
            };

            _session.Events.Append(Guid.NewGuid(), aggregate.Approve());
            await _session.SaveChangesAsync();
        }

        public async Task RejectByOrderAsync(string userId, Order order)
        {
            var aggregate = new DeliveryApproval
            {
                Order = order,
                UserId = userId
            };

            _session.Events.Append(Guid.NewGuid(), aggregate.Reject());
            await _session.SaveChangesAsync();
        }
    }
}