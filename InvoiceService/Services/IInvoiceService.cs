using InvoiceService.Dtos;
using InvoiceService.Models;

namespace InvoiceService.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int id);
        Task<Invoice> CreateAsync(CreateInvoiceDto dto);
        Task<Invoice?> UpdateAsync(int id, UpdateInvoiceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
