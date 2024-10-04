using DAL.Abstract;
using DTO;
using System.Data.SqlClient;

namespace DAL.Beton
{
    public class WareInventoryDAL : IWareInventoryDAL
    {
        private readonly SqlConnection _connection;

        public WareInventoryDAL(string connectionString)
        { _connection = new SqlConnection(connectionString); }
        public WareInventory Add(WareInventory wareInventory)
        {
            throw new NotImplementedException();
        }

        public List<WareInventory> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT wareID, inventoryID FROM tblWareInventory";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<WareInventory> wareInventories = new List<WareInventory>();
                while (reader.Read())
                {
                    wareInventories.Add(new WareInventory
                    {
                        WareID = Convert.ToInt16(reader["wareID"]),
                        InventoryID = Convert.ToInt16(reader["inventoryID"])
                    });
                }
                _connection.Close();
                return wareInventories;
            }
        }
    }
}
