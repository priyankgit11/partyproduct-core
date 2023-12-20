using System;
using System.Collections.Generic;

namespace PartyProductCore.Models;

public partial class TblAssignParty
{
    public int Id { get; set; }

    public int PartyId { get; set; }

    public int ProductId { get; set; }

    public virtual TblParty Party { get; set; } = null!;

    public virtual TblProduct Product { get; set; } = null!;
}
