using System;
using System.Collections.Generic;

namespace DALef.Models;

public partial class TblInventory
{
    public int WareId { get; set; }

    public string WareName { get; set; } = null!;

    public int Count { get; set; }

    public virtual TblWare Ware { get; set; } = null!;
}
