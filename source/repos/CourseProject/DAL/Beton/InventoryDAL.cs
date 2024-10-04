using DAL.Abstract;
using DTO;
using System.Data.SqlClient;

namespace DAL.Beton
{
    public class InventoryDAL : IInventoryDAL
    {
        private readonly SqlConnection _connection;

        public InventoryDAL(string connectionString)
        { _connection = new SqlConnection(connectionString); }
        public Inventory Add(Inventory inventory)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO tblInventory (cityID) output inserted.inventoryID VALUES (@cityID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cityID", inventory.CityID);
                _connection.Open();
                inventory.InventoryID = Convert.ToInt16(cmd.ExecuteScalar());
                _connection.Close();
                return inventory;
            }
        }

        public short DeleteByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM tblInventory WHERE inventoryID = @inventoryID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("inventoryID", id);

                _connection.Open();

                short rows = (short)cmd.ExecuteNonQuery();
                _connection.Close();
                return rows;
            }
        }

        public List<Inventory> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT inventoryID, cityID FROM tblInventory";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Inventory> inventories = new List<Inventory>();
                while (reader.Read())
                {
                    inventories.Add(new Inventory
                    {
                        InventoryID= Convert.ToInt16(reader["inventoryID"]),
                        CityID = Convert.ToInt16(reader["cityID"]),
                    });
                }
                _connection.Close();
                return inventories;
            }
        }

        public Inventory GetByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT inventoryID, cityID FROM tblInventory WHERE inventoryID = @inventoryID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("inventoryID", id);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                    return new Inventory
                    {
                        InventoryID = Convert.ToInt16(reader["inventoryID"]),
                        CityID = Convert.ToInt16(reader["cityID"])
                    };
                return null;
            }
        }

        public void Update(Inventory inventory)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE tblInventory SET cityID = @cityID WHERE inventoryID = @inventoryID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cityID", inventory.CityID);
                cmd.Parameters.AddWithValue("inventoryID", inventory.InventoryID);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
