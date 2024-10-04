using DAL.Abstract;
using DTO;
using System.Data.SqlClient;

namespace DAL.Beton
{
    public class CityDAL : ICityDAL
    {
        private readonly SqlConnection _connection;

        public CityDAL(string connectionString)
        { _connection = new SqlConnection(connectionString); }
        public City Add(City city)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO tblCity (cityName) output inserted.cityID VALUES (@cityName)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cityName", city.CityName);
                _connection.Open();
                city.CityID = Convert.ToInt16(cmd.ExecuteScalar());
                _connection.Close();
                return city;
            }
        }

        public short DeleteByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM tblCity WHERE cityID = @cityID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cityID", id);

                _connection.Open();

                short rows = (short)cmd.ExecuteNonQuery();
                _connection.Close();
                return rows;
            }
        }

        public List<City> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT cityID, cityName FROM tblCity";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<City> cities = new List<City>();
                while(reader.Read())
                {
                    cities.Add(new City
                    {
                        CityID = Convert.ToInt16(reader["cityID"]),
                        CityName = reader["cityName"].ToString()
                    });
                }
                _connection.Close();
                return cities;
            }
        }

        public City GetByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT cityID, cityName FROM tblCity WHERE cityID = @cityID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cityID", id);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                    return new City
                    {
                        CityID = Convert.ToInt16(reader["cityID"]),
                        CityName = reader["cityName"].ToString()
                    };
                return null;
            }
        }

        public void Update(City city)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE tblCity SET cityName = @cityName WHERE cityID = @cityID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cityName", city.CityName);
                cmd.Parameters.AddWithValue("cityID", city.CityID);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
