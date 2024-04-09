using AutoMapper;
using Core.Crud.Services;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<SelectListItem>> GetDropdownItems()
        {
            var query = base.GetAll();
            return await query.OrderBy(q => q.Sequence).Select(q => 
            new SelectListItem() { 
                Icon=q.Icon,
                Sequence= q.Sequence,
                Value= q.Id.ToString(),
                Text= q.Name,
                }
            ).ToListAsync();
        }
    }
}
