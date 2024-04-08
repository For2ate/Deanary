using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public short AccessLevel { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Contactphone> Contactphones { get; set; } = new List<Contactphone>();
}
