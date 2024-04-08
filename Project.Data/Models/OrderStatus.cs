using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
    }
}
