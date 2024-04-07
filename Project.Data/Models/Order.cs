using System.Security.Principal;
using Core.Crud.Data;

namespace Project.Data.Models
{
    public class Order: IEntity
    {
        public int Id { get; set; }
        public string PickupLocation { get; set; }
    }
}
