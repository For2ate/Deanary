using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class TypeStatus {
    public int TypeId { get; set; }

    public string StatusType { get; set; } = null!;
}
