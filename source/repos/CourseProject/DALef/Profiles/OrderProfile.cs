using AutoMapper;
using DALef.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALef.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<TblOrder, Order>();
            CreateMap<Order,TblOrder>();
        }

    }
}
