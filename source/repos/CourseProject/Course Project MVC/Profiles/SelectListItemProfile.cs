using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Course_Project_MVC.Profiles
{
    public class SelectListItemProfile:Profile
    {
        public SelectListItemProfile()
        {
            CreateMap<Ware, SelectListItem>()
                .ForMember(d => d.Value, s => s.MapFrom(w => w.WareID))
                .ForMember(d => d.Text, s => s.MapFrom(w => w.WareName + ": ₿" + w.Cost.ToString()));
        }
    }
}
