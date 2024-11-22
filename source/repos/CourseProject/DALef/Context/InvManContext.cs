using DALef.Models;
using Microsoft.EntityFrameworkCore;

namespace DALef.Context
{
    public class InvManContext:InventoryManagerContext
    {
        private readonly string connectionString;
        public InvManContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(connectionString);
    }
}
