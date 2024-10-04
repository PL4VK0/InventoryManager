namespace DTO
{
    public class City
    {
        private short cityID;

        private string cityName;
        public short CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }
        public string CityName
        {
            get {return cityName;}
            set { cityName = value;}
        }
        public override string ToString()
        {
            return $"{cityID} - {cityName}";
        }
    }
}
