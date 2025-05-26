namespace InvoiceService.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime IssuedDate { get; set; } 
        public string Status { get; set; } = null!;

    }
}
