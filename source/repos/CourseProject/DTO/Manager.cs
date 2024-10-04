using System.Runtime.Remoting;

namespace DTO
{
    public class Manager
    {
        private short managerID;
        private string firstName;
        private string lastName;
        private string userName;
        private string password;
        private short inventoryID;

        public short ManagerID
        {
            get { return managerID; }
            set { managerID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public short InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }

        public override string ToString()
        {
            return $"{managerID} - {firstName} {lastName}, AKA {userName} works at inventory {inventoryID}";
        }


        public override bool Equals(object? obj)
        {
            return obj is Manager manager && Equals((Manager)obj);
        }
        public bool Equals(Manager other)
        {
            return other.FirstName == firstName && 
                   other.LastName == lastName&&
                   other.UserName == userName&&
                   other.Password == password&&
                   other.InventoryID == inventoryID&&
                   other.ManagerID==managerID;
        }
    }
}
