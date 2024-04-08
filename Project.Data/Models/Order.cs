using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Core.Crud.Data;

namespace Project.Data.Models
{
    public class Order: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? ReferenceId { get; set; }
        public string Cargo { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Shipper { get; set; }
        public string OriginAirport { get; set; }
        public string Consignee{ get; set; }
        public string DestinationAirport { get; set; }
        public string OriginInlandAirport { get; set; }
        public string DestinationInlandAirport { get; set; }
        public int? OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string AgentAssigned { get; set; }
        public string Comments { get; set; }
    }
}
