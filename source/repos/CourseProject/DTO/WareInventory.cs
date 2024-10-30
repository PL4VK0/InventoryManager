namespace DTO
{
    public class WareInventory
    {
        private short wareID;
        private short count;
        public short Count
        {
            get { return count; }
            set { count = value; }
        }

        public short WareID
        {
            get { return wareID; }
            set { wareID = value; }
        }
    }
}
