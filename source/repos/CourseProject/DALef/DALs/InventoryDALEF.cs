using AutoMapper;
using DAL.Abstract;
using DALef.Context;
using DALef.Models;
using DTO;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace DALef.DALs
{
    public class InventoryDALEF:IWareInventoryDAL
    {
        private readonly string connectionString;
        private readonly IMapper mapper;
        public InventoryDALEF(IMapper mapper, string connectionString)
        {
            this.connectionString = connectionString;
            this.mapper = mapper;
        }

        public WareInventory Add(WareInventory wareInventory)
        {
            throw new NotImplementedException();
        }

        public short DeleteByID(short id)
        {
            throw new NotImplementedException();
        }

        public List<WareInventory> GetAll()
        {
            using (var context = new InvManContext(connectionString))
            {
                var inventory = context.TblInventories.ToList();
                return mapper.Map<List<WareInventory>>(inventory);
            }
        }

        public WareInventory GetByID(short id)
        {
            using (var context = new InvManContext(connectionString))
                return mapper.Map<WareInventory>(context.TblInventories.Where(i=>i.WareId==id).FirstOrDefault());
        }

        public void Update(WareInventory wareInventory)
        {
            using (var context = new InvManContext(connectionString))
            {
                context.Update(mapper.Map<TblInventory>(wareInventory));
                context.SaveChanges();  
            }
        }
    }
}
