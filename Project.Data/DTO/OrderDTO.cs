
using Project.Data.Models;

namespace Project.Data.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string? ReferenceId { get; set; }
        public string? Cargo { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Shipper { get; set; }
        public string? Consignee { get; set; }
        public string? OriginAirport { get; set; }
        public string? OriginInlandAirport { get; set; }
        public string? DestinationAirport { get; set; }
        public string? DestinationInlandAirport { get; set; }
        public int? OrderStatusId { get; set; }
        public string? OrderStatusName { get; set; }
        public string? AgentAssigned { get; set; }
        public string? Comments { get; set; }
    }
}
