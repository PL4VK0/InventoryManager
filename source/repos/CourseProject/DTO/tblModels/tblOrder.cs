namespace WPF.ViewModels
{
    public class tblOrder
    {
        private short orderID;
        private short wareID;
        private string wareName;
        private short managerID;
        private string managerUserName;
        private short count;
        private DateTime date;

        public short OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        public short WareID  
        {
            get { return wareID; }
            set { wareID = value; }
        }
        public string WareName
        {
            get { return wareName; }
            set { wareName = value; }
        }
        public short ManagerID
        {
            get { return managerID; }
            set { managerID = value; }
        }
        public string ManagerUserName
        {
            get { return managerUserName; }
            set { managerUserName = value; }
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
    }
}
