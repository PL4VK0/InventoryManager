using AutoMapper;
using DALef.Models;
using DTO;

namespace DALef.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<TblOrder, Order>()
                .ForMember(d => d.UserName, s => s.MapFrom(m => m.Manager.UserName))
                .ForMember(d => d.WareName, s => s.MapFrom(m => m.Ware.WareName));
            CreateMap<Order, TblOrder>();
        }

    }
}
