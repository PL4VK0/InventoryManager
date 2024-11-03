namespace DTO
{
    public class Order
    {
        private short orderID;
        private short managerID;
        private short wareID;
        private short count;
        private DateTime date = DateTime.UtcNow;
        public short WareID
        {
            get { return wareID; }
            set { wareID = value; }
        }
        public short Count
        {
            get { return count; }
            set { count = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public short OrderID
        { 
            get { return orderID; }
            set { orderID = value; } 
        }
        public short ManagerID
        {
            get { return managerID; }
            set { managerID = value; }
        }

        public override string ToString()
        {
            return $"OrderID - {orderID}, ManagerID - {managerID}, Date - {Date}";
        }
    }
}
