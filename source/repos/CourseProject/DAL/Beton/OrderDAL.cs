using DAL.Abstract;
using DTO;
using System.Data.SqlClient;

namespace DAL.Beton
{
    public class OrderDAL : IOrderDAL
    {
        private readonly SqlConnection _connection;

        public OrderDAL(string connectionString)
        { _connection = new SqlConnection(connectionString); }
        public Order Add(Order order)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO tblOrder (managerID, wareID, count, date) output inserted.orderID VALUES (@managerID, @wareID, @count, @date)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("managerID", order.ManagerID);
                cmd.Parameters.AddWithValue("wareID", order.WareID);
                cmd.Parameters.AddWithValue("count", order.Count);
                cmd.Parameters.AddWithValue("date", order.Date);
                _connection.Open();
                order.OrderID = Convert.ToInt16(cmd.ExecuteScalar());
                _connection.Close();
                return order;
            }
        }

        public short DeleteByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM tblOrder WHERE orderID = @orderID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("orderID", id);

                _connection.Open();

                short rows = (short)cmd.ExecuteNonQuery();
                _connection.Close();
                return rows;
            }
        }

        public List<Order> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT orderID, managerID, wareID, count, date FROM tblOrder";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Order> orders = new List<Order>();
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        OrderID = Convert.ToInt16(reader["orderID"]),
                        ManagerID = Convert.ToInt16(reader["managerID"]),
                        WareID = Convert.ToInt16(reader["wareID"]),
                        Count = Convert.ToInt16(reader["count"]),
                        Date = Convert.ToDateTime(reader["date"])
                    });
                }
                _connection.Close();
                return orders;
            }
        }

        public Order GetByID(short id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT orderID, managerID, wareID, count, date FROM tblOrder WHERE orderID = @orderID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("orderID", id);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                    return new Order
                    {
                        OrderID = Convert.ToInt16(reader["orderID"]),
                        ManagerID = Convert.ToInt16(reader["managerID"]),
                        WareID = Convert.ToInt16(reader["wareID"]),
                        Count = Convert.ToInt16(reader["count"]),
                        Date = Convert.ToDateTime(reader["date"])
                    };
                _connection.Close();
                return null;
            }
        }

        public void Update(Order order)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE tblOrder SET managerID = @managerID, wareID = @wareID, count = @count, date = @date WHERE orderID = @orderID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("orderID",order.OrderID);
                cmd.Parameters.AddWithValue("managerID", order.ManagerID);
                cmd.Parameters.AddWithValue("wareID", order.WareID);
                cmd.Parameters.AddWithValue("count",order.Count);
                cmd.Parameters.AddWithValue("date",order.Date);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return;
        }
    }
}
