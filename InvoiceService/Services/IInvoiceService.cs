using InvoiceService.Models;

namespace InvoiceService.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int id);
        Task<Invoice> CreateAsync(Invoice invoice);
        Task<bool> UpdateAsync(int id, Invoice invoice);
        Task<bool> DeleteAsync(int id);
    }
}
