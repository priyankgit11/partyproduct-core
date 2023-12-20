namespace PartyProductCore.DTO
{
    public class InvoiceDetailsDTO
    {
        public int Id { get; set; }
        public int PartyId { get; set; }
        public int ProductId { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
    }
}
