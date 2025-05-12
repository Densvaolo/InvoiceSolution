namespace InvoiceService.Dtos
{
    public class CreateInvoiceDto
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string EventName { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // t.ex. "Unpaid"
    }
}
