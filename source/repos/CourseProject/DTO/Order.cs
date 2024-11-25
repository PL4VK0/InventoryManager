namespace DTO
{
    public class Order
    {
        private short orderID;
        private short managerID;
        private short wareID;
        private short count;
        private string wareName;
        private string userName;
        private DateTime date = DateTime.UtcNow;
        public string WareName
        {
            get { return wareName; }
            set { wareName = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
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
            return $"OrderID - {orderID}, ManagerID - {managerID}, WareID - {WareID}, Count - {Count}, Date - {Date}";
        }
        public override bool Equals(object? obj)
        {
            var other = obj as Order;
            return other.WareID == WareID &&
                other.Count == Count &&
                //other.Date == Date &&
                other.OrderID == OrderID &&
                other.ManagerID == ManagerID;
        }
    }
}
