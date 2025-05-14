using InvoiceService.Dtos;
using InvoiceService.Models;

namespace InvoiceService.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto?> GetByIdAsync(int id);
        Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto);
        Task<InvoiceDto?> UpdateAsync(int id, UpdateInvoiceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
