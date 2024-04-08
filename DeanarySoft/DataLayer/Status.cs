using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer;

public partial class Status
{
    public int TypeId { get; set; }

    public string StatusType { get; set; } = null!;
}
