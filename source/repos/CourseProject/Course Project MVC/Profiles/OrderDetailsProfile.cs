using AutoMapper;
using Course_Project_MVC.Models;
using DTO;

namespace Course_Project_MVC.Profiles
{
    public class OrderDetailsProfile:Profile
    {
        public OrderDetailsProfile()
        {
            CreateMap<Order, OrderDetailsModel>();
            CreateMap<Order, EditOrderModel>();
            CreateMap<EditOrderModel, Order>();
        }
    }
}
