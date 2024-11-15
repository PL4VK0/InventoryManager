namespace DTO
{
    public class WareInventory
    {
        private short wareID;
        private string wareName;
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
        public string WareName
        {
            get { return wareName; }
            set { wareName = value; }
        }
        public override string ToString()
        {
            return $"ID - {WareID}, Name - {WareName}, Count - {Count}";
        }
        public override bool Equals(object? obj)
        {
            var other = obj as WareInventory;
            if (other == null) return false;
            return other.Count == Count &&
                other.WareID == WareID &&
                other.WareName == WareName;
        }
    }
}
