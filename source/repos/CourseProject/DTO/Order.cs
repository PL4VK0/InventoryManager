namespace DTO
{
    public class Order
    {
        private short orderID;
        private short managerID;
        private short inventoryID;


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
        public short InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }

        public override string ToString()
        {
            return $"OrderID - {orderID}, ManagerID - {managerID}, InventoryID - {InventoryID}";
        }
    }
}
