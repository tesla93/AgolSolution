using Core.Crud.Services;
using Project.Data.DTO;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface IOrderStatusService : ICrudService<OrderStatus, OrderStatusDTO, int>
    {
        Task<List<SelectListItem>> GetDropdownItems();
    }
}
