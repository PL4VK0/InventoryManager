using DAL.Beton;
using DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Course_Project_MVC.Models
{
    public class OrderDetailsModel
    {
        public OrderDetailsModel()
        {
            OrderID = 0;
            UserName = string.Empty;
            WareName = string.Empty;
            WareID = 0;
            ManagerID = 0;
            Count = 0;
            Date = DateTime.MinValue;
        }
        public short OrderID { get; set; }
        [DisplayName("Manager's Username")]
        public string UserName { get; set; }

        [DisplayName("Ware Name")]
        public string WareName { get; set; }
        public short WareID { get; set; }
        public short ManagerID { get; set; }

        public short Count { get; set; }
        public DateTime Date { get; set; }
    }
}
