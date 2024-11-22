using AutoMapper;
using Course_Project_MVC.Models;
using DTO;

namespace Course_Project_MVC.Profiles
{
    public class InventoryDetailsProfile:Profile
    {
        public InventoryDetailsProfile()
        {
            CreateMap<WareInventory, InventoryDetailsModel>();
        }
    }
}
