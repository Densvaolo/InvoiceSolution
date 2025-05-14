namespace InvoiceService.Dtos
{
    public class UpdateInvoiceDto
{
    public string Status { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
}

}
