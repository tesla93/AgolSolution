using Core.Crud.Services;
using Project.Data.DTO;
using Project.Data.Models;

namespace Project.Services.Interfaces
{
    public interface IOrderService : ICrudService<Order, OrderDTO, int>
    {
    }
}
