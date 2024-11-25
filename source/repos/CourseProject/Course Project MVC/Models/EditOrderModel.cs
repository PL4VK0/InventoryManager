using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Course_Project_MVC.Models
{
    public class EditOrderModel
    {
        public short OrderID { get; set; }
        public short ManagerID { get; set; }
        public string WareName { get; set; }
        public string UserName { get; set; }
        [DisplayName("Ware Info")]
        public short WareID { get; set; }
        [Required(ErrorMessage = "At least order 1")]
        [Range(1, 100, ErrorMessage = "Count should be between 1 and 100\n(no, you can't order 83475623897 items)")]
        public short Count { get; set; }

        public DateTime Date {  get; set; }

        public List<SelectListItem> Wares { get; set; }
        public EditOrderModel()
        {
            OrderID = 0;
            WareID = 0;
            ManagerID = 0;
            Date = DateTime.MinValue;
            Count = 0;
            Wares = new List<SelectListItem>();
            UserName = string.Empty;
            WareName = string.Empty;
        }
    }
}
