namespace Orders.Api.Domain.Entities
{
    public class Delivery
    {
        public string DeliveryId { get; set; }
        public string DeliveryNumber { get; set; }
        public string OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerId { get; set; }
        public string TruckId { get; set; }
        public string TruckName { get; set; }
        public string PlantId { get; set; }
    }
}