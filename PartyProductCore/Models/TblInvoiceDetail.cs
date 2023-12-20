using System;
using System.Collections.Generic;

namespace PartyProductCore.Models;

public partial class TblInvoiceDetail
{
    public int Id { get; set; }

    public int PartyId { get; set; }

    public int ProductId { get; set; }

    public decimal Rate { get; set; }

    public int Quantity { get; set; }

    public virtual TblParty Party { get; set; } = null!;

    public virtual TblProduct Product { get; set; } = null!;
}
