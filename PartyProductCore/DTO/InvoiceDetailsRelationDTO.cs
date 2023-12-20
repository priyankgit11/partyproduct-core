namespace PartyProductCore.DTO
{
    public class InvoiceDetailsRelationDTO : InvoiceDetailsDTO
    {
        public string PartyName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
    }
}
