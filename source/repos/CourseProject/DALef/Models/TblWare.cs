using System;
using System.Collections.Generic;

namespace DALef.Models;

public partial class TblWare
{
    public int WareId { get; set; }

    public string? WareName { get; set; }

    public string? WareDescription { get; set; }

    public float? WareCost { get; set; }
}
