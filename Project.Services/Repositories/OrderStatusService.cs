using AutoMapper;
using Core.Crud.Services;
using Project.Data;
using Project.Data.DTO;
using Project.Data.Models;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Repositories
{
    public class OrderStatusService: CrudService<OrderStatus, OrderStatusDTO, int>, IOrderStatusService
    {
        private readonly DataContextBase _context;
        private readonly IMapper _mapper;
        public OrderStatusService(DataContextBase context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
