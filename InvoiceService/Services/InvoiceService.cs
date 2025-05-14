using InvoiceService.Models;
using InvoiceService.Repositories;
using InvoiceService.Dtos;
using Microsoft.EntityFrameworkCore;


namespace InvoiceService.Services
{
    public class InvoiceService(IInvoiceRepository repository) : IInvoiceService
    {
        private readonly IInvoiceRepository _repository = repository;

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync();

            return invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                CustomerName = i.CustomerName,
                EventName = i.EventName,
                Amount = i.Amount,
                DueDate = i.DueDate,
                Status = i.Status
            });
        }



        public async Task<InvoiceDto?> GetByIdAsync(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice == null) return null;

            return new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = invoice.CustomerName,
                EventName = invoice.EventName,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Status = invoice.Status
            };
        }

        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = $"INV{DateTime.Now:yyyyMMddHHmmss}",
                BookingId = dto.BookingId,
                CustomerName = dto.CustomerName,
                EventName = dto.EventName,
                Amount = dto.Amount,
                IssuedDate = dto.IssuedDate,
                DueDate = dto.DueDate,
                Status = dto.Status
            };

            var created = await _repository.AddAsync(invoice);

            return new InvoiceDto
            {
                Id = created.Id,
                InvoiceNumber = created.InvoiceNumber,
                CustomerName = created.CustomerName,
                EventName = created.EventName,
                Amount = created.Amount,
                DueDate = created.DueDate,
                Status = created.Status
            };
        }



        public async Task<InvoiceDto?> UpdateAsync(int id, UpdateInvoiceDto dto)
        {
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice == null) return null;

            invoice.Status = dto.Status;
            invoice.DueDate = dto.DueDate;

            await _repository.SaveChangesAsync();

            return new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = invoice.CustomerName,
                EventName = invoice.EventName,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Status = invoice.Status
            };
        }


        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id)) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private InvoiceDto MapToDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = invoice.CustomerName,
                EventName = invoice.EventName,
                Amount = invoice.Amount,
                Status = invoice.Status,
                DueDate = invoice.DueDate
            };
        }



    }
}
