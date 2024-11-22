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
    public class InventoryProfile:Profile
    {
        public InventoryProfile()
        {
            CreateMap<TblInventory, WareInventory>();
            CreateMap<WareInventory, TblInventory>();
        }
    }
}


