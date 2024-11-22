using System;
using System.Collections.Generic;

namespace DALef.Models;

public partial class TblManager
{
    public int ManagerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string Salt { get; set; } = null!;
}
