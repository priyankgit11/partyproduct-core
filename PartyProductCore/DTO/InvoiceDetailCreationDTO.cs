namespace PartyProductCore.DTO
{
    public class InvoiceDetailCreationDTO
    {
        public int ProductId { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public int PartyId { get; set; }
    }
}
