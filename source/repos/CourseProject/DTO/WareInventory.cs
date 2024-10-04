namespace DTO
{
    public class WareInventory
    {
        private short wareID;
        private short inventoryID;

        public short WareID
        {
            get { return wareID; }
            set { wareID = value; }
        }
        public short InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }
    }
}
