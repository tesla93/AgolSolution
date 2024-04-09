using AutoMapper;
using Project.Data.DTO;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Configuration
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(d => d.OrderStatusName, opt => opt.MapFrom(src => src.OrderStatus.Name))
                //.ForMember(d => d.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ReverseMap()
                .ForMember(d => d.OrderStatus, opt => opt.Ignore());
            CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
        }
    }
}
