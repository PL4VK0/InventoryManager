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
                cmd.CommandText = "INSERT INTO tblOrder (inventoryID, managerID) output inserted.orderID VALUES (@inventoryID, @managerID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("inventoryID", order.InventoryID);
                cmd.Parameters.AddWithValue("managerID", order.ManagerID);
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
                cmd.CommandText = "SELECT orderID, managerID, inventoryID FROM tblOrder";

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Order> orders = new List<Order>();
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        OrderID = Convert.ToInt16(reader["orderID"]),
                        InventoryID = Convert.ToInt16(reader["inventoryID"]),
                        ManagerID = Convert.ToInt16(reader["managerID"])
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
                cmd.CommandText = "SELECT orderID, managerID,inventoryID FROM tblOrder WHERE orderID = @orderID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("orderID", id);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                    return new Order
                    {
                        OrderID = Convert.ToInt16(reader["orderID"]),
                        ManagerID = Convert.ToInt16(reader["managerID"]),
                        InventoryID = Convert.ToInt16(reader["inventoryID"])
                    };
                return null;
            }
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
