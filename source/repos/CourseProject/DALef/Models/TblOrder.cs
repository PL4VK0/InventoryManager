using System;
using System.Collections.Generic;

namespace DALef.Models;

public partial class TblOrder
{
    public int OrderId { get; set; }

    public int ManagerId { get; set; }

    public int WareId { get; set; }

    public int Count { get; set; }

    public DateTime Date { get; set; }
}
