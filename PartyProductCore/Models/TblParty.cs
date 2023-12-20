using System;
using System.Collections.Generic;

namespace PartyProductCore.Models;

public partial class TblParty
{
    public int Id { get; set; }

    public string PartyName { get; set; } = null!;

    public virtual ICollection<TblAssignParty> TblAssignParties { get; set; } = new List<TblAssignParty>();

    public virtual ICollection<TblInvoiceDetail> TblInvoiceDetails { get; set; } = new List<TblInvoiceDetail>();
}
