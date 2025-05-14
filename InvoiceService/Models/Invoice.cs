namespace InvoiceService.Models
{
    public class Invoice
    {
        public int Id { get; set; } //ändra till string och byt namn till InvoiceID och = guid 
        public string InvoiceNumber { get; set; } = null!;
        public int BookingId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "Unpaid"; // kolla status en egen entity?
        

    }
}
