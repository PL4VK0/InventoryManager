using AutoMapper;
using Course_Project_MVC.Models;
using DTO;
using WPF.ViewModels;

namespace Course_Project_MVC.Profiles
{
    public class OrderDetailsProfile:Profile
    {
        public OrderDetailsProfile()
        {
            CreateMap<Order, OrderDetailsModel>();
            //.ForMember(d=>d.WareName,s=>s.MapFrom(o=>o.WareID))
            //.ForMember(d=>d.UserName,s=>s.MapFrom(o=>o.ManagerID));
            CreateMap<Order, EditOrderModel>();
            //.ForMember(d => d.WareName, s => s.MapFrom(o => o.WareID))
            //.ForMember(d => d.UserName, s => s.MapFrom(o => o.ManagerID));
            CreateMap<EditOrderModel, Order>();
            //.ForMember(d => d.WareID, s => s.MapFrom(m => m.WareName))
            //.ForMember(d => d.ManagerID, s => s.MapFrom(m => m.UserName));
        }
    }
}
