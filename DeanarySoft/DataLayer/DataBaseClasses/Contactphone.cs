using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Contactphone {
    public int Contact { get; set; }

    public int StaffId { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
