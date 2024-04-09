using Core.Crud.Data;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class OrderStatus: IEntity
    {
        [Key]
        public int Id { get; set; }
        public int? Sequence { get; set; }
        public string? Icon { get; set; }
        public string? Name { get; set; }
    }
}
