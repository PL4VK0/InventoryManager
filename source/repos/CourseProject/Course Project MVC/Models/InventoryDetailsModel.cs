namespace Course_Project_MVC.Models
{
    public class InventoryDetailsModel
    {
        public short WareID { get; set; }
        public short Count { get; set; }
        public InventoryDetailsModel()
        {
            WareID = 0;
            Count = 0;
        }
    }
}
