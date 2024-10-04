namespace DTO
{
    public class Inventory
    {
        private short inventoryID;
        private short cityID;

        public short InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }

        public short CityID
        {
            get {return cityID;}
            set { cityID = value;}
        }

        public override string ToString()
        {
            return $"ID - {inventoryID} City ID - {cityID}";
        }
    }
}
