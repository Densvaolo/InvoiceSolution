using InvoiceService.Models;
using InvoiceService.Repositories;
using InvoiceService.Dtos;

namespace InvoiceService.Services
{
    public class InvoiceService(IInvoiceRepository repository) : IInvoiceService
    {
        private readonly IInvoiceRepository _repository = repository;

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Invoice> CreateAsync(CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = $"INV{DateTime.Now:yyyyMMddHHmmss}", // t.ex. INV20240507185000
                BookingId = dto.BookingId,
                CustomerName = dto.CustomerName,
                EventName = dto.EventName,
                Amount = dto.Amount,
                IssuedDate = dto.IssuedDate,
                DueDate = dto.DueDate,
                Status = dto.Status
            };

            return await _repository.AddAsync(invoice);
        }




        public async Task<bool> UpdateAsync(int id, Invoice invoice)
        {
            if (!await _repository.ExistsAsync(id)) return false;

            invoice.Id = id;
            await _repository.UpdateAsync(invoice);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id)) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
