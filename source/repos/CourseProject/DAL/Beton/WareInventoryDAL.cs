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
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO tblInventory (wareID,wareName,count) VALUES (@wareID,@wareName,@count)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareID", wareInventory.WareID);
                cmd.Parameters.AddWithValue("wareName",wareInventory.WareName);
                cmd.Parameters.AddWithValue("count", wareInventory.Count);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
                return wareInventory;
            }
        }

        public short DeleteByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM tblInventory WHERE wareID = @wareID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareID", id);

                _connection.Open();
                short rows = (short)cmd.ExecuteNonQuery();
                _connection.Close();
                return rows;
            }
        }

        public List<WareInventory> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT wareID,wareName,count FROM tblInventory";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<WareInventory> wareInventories = new List<WareInventory>();
                while (reader.Read())
                {
                    wareInventories.Add(new WareInventory
                    {
                        WareID = Convert.ToInt16(reader["wareID"]),
                        WareName = reader["wareName"].ToString(),
                        Count = Convert.ToInt16(reader["count"])
                    });
                }
                _connection.Close();
                return wareInventories;
            }
        }

        public WareInventory GetByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT wareID,wareName,count FROM tblInventory WHERE wareID = @wareID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareID", id);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                WareInventory gotInventoryItem = null;
                if (reader.Read())
                    gotInventoryItem = new WareInventory
                    {
                        WareID = Convert.ToInt16(reader["wareID"]),
                        WareName= reader["wareName"].ToString(),
                        Count = Convert.ToInt16(reader["count"].ToString())
                    };
                _connection.Close();
                return gotInventoryItem;
            }
        }

        public void Update(WareInventory wareInventory)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE tblInventory SET count = @count WHERE wareID = @wareID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareID", wareInventory.WareID);
                cmd.Parameters.AddWithValue("count", wareInventory.Count);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
