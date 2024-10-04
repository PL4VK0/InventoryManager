﻿namespace DTO
{
    public class Ware
    {
        private short wareID;
        private string wareName;
        private string wareDescription;
        private float cost;

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
        public string WareDescription
        {
            get { return wareDescription; }
            set { wareDescription = value; }
        }

        public override string ToString()
        {
            return $"{wareID} - {wareName}, {wareDescription}, price - {cost} (in bitcoins)";
        }

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }
    }
}