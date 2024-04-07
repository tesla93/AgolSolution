using Project.Data.DTO;
using Project.Data.Models;
using Project.Services.Interfaces;
using Core.Crud.Services;
using Project.Data;
using AutoMapper;

namespace Project.Services.Repositories
{
    public class OrderService : CrudService<Order, OrderDTO, int>, IOrderService  
    {
        private readonly DataContextBase _context;
        private readonly IMapper _mapper;
        public OrderService(DataContextBase context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
