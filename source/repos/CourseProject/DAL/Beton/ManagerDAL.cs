using DAL.Abstract;
using DTO;
using Services;
using System.Data.SqlClient;

namespace DAL.Beton
{
    public class ManagerDAL : IManagerDAL
    {
        private readonly SqlConnection _connection;

        public ManagerDAL(string connectionString)
        { _connection = new SqlConnection(connectionString);}

        public Manager Add(Manager manager)
        {
            Byte[] password = HashOperations.GetHash(manager.Password + manager.Salt);
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO tblManager (firstName,lastName,userName,password,salt) output inserted.managerID VALUES (@firstName,@lastName,@userName,@password,@salt)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("firstName", manager.FirstName);
                cmd.Parameters.AddWithValue("lastName", manager.LastName);
                cmd.Parameters.AddWithValue("userName", manager.UserName);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("salt", manager.Salt);
                _connection.Open();
                manager.ManagerID = Convert.ToInt16(cmd.ExecuteScalar());
                _connection.Close();
                manager.Password = password.ToString();
                return manager;
            }
        }

        public short DeleteByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM tblManager WHERE managerID = @managerID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("managerID", id);

                _connection.Open();

                short rows = (short)cmd.ExecuteNonQuery();
                _connection.Close();
                return rows;
            }
        }

        public List<Manager> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT managerID, firstName, lastName, userName, password FROM tblManager";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Manager> managers = new List<Manager>();
                while (reader.Read())
                {
                    managers.Add(new Manager
                    {
                        ManagerID = Convert.ToInt16(reader["managerID"]),
                        FirstName = reader["firstName"].ToString(),
                        LastName = reader["lastName"].ToString(),
                        UserName = reader["userName"].ToString(),
                        Password = BitConverter.ToString(reader["password"] as Byte[]).Replace("-", "")
                    });
                }
                _connection.Close();
                return managers;
            }
        }

        public Manager GetByID(short id)
        {
            _connection.Open();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT managerID, firstName,lastName,userName,password FROM tblManager WHERE managerID = @managerID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("managerID", id);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    Manager manager = new Manager
                    {
                        ManagerID = Convert.ToInt16(reader["managerID"]),
                        FirstName = reader["firstName"].ToString(),
                        LastName = reader["lastName"].ToString(),
                        UserName = reader["userName"].ToString(),
                        Password = BitConverter.ToString(reader["password"] as Byte[]).Replace("-", "")
                    };
                    _connection.Close();
                    return manager;
                }
                _connection.Close ();
                return null;
            }
        }

        public string GetPassword(Manager manager)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM tblManager WHERE managerID=@managerID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("managerID", manager.ManagerID);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string password = "";
                if (reader.Read())
                    password = BitConverter.ToString(reader["password"] as Byte[]).Replace("-","");
                _connection.Close();
                return password;
            }
        }

        public string GetSalt(Manager manager)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM tblManager WHERE managerID=@managerID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("managerID", manager.ManagerID);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string salt = "";
                if(reader.Read())
                    salt = Convert.ToDateTime(reader["salt"]).ToString();
                _connection.Close();
                return salt;
            }
        }

        public void Update(Manager manager)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE tblManager SET firstName = @firstName,lastName = @lastName, userName = @userName, password = @password WHERE managerID = @managerID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("managerID", manager.ManagerID);
                cmd.Parameters.AddWithValue("firstName", manager.FirstName);
                cmd.Parameters.AddWithValue("lastName", manager.LastName);
                cmd.Parameters.AddWithValue("userName", manager.UserName);
                cmd.Parameters.AddWithValue("password",manager.Password);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
