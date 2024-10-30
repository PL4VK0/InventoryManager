using DAL.Abstract;
using DTO;
using System.Data.SqlClient;

namespace DAL.Beton
{
    public class WareDAL : IWareDAL
    {
        private readonly SqlConnection _connection;

        public WareDAL(string connectionString)
        { _connection = new SqlConnection(connectionString); }
        public Ware Add(Ware ware)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO tblWare (wareName,wareDescription,wareCost) output inserted.wareID VALUES (@wareName,@wareDescription,@cost)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareName", ware.WareName);
                cmd.Parameters.AddWithValue("wareDescription", ware.WareDescription);
                cmd.Parameters.AddWithValue("cost", ware.Cost);
                _connection.Open();
                ware.WareID = Convert.ToInt16(cmd.ExecuteScalar());
                _connection.Close();
                return ware;
            }
        }

        public short DeleteByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM tblWare WHERE wareID = @wareID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareID", id);

                _connection.Open();

                short rows = (short)cmd.ExecuteNonQuery();
                _connection.Close();
                return rows;
            }
        }

        public List<Ware> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT wareID, wareName, wareDescription, wareCost FROM tblWare";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Ware> wares = new List<Ware>();
                while (reader.Read())
                {
                    wares.Add(new Ware
                    {
                        WareID = Convert.ToInt16(reader["wareID"]),
                        WareName = reader["wareName"].ToString(),
                        WareDescription = reader["wareDescription"].ToString(),
                        Cost =(float)Convert.ToDouble(reader["wareCost"])
                    });
                }
                _connection.Close();
                return wares;
            }
        }

        public Ware GetByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT wareID, wareName,wareDescription FROM tblWare WHERE wareID = @wareID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareID", id);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                    return new Ware
                    {
                        WareID = Convert.ToInt16(reader["wareID"]),
                        WareName = reader["wareName"].ToString(),
                        WareDescription = reader["wareDescription"].ToString()
                    };
                return null;
            }
        }

        public void Update(Ware ware)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE tblWare SET wareName = @wareName,wareDescription = @wareDescription, wareCost = @cost";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("wareName", ware.WareName);
                cmd.Parameters.AddWithValue("wareDescription", ware.WareDescription);
                cmd.Parameters.AddWithValue("cost", ware.Cost);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
