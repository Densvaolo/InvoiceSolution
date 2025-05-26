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
                IssuedDate = i.IssuedDate, 
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
                IssuedDate = invoice.IssuedDate,
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

            Console.WriteLine("📩 DTO I API:");
            Console.WriteLine($"IssuedDate: {dto.IssuedDate}");
            Console.WriteLine($"Amount: {dto.Amount}");
            Console.WriteLine($"CustomerName: {dto.CustomerName}");
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice == null) return null;

            invoice.BookingId = dto.BookingId;
            invoice.CustomerName = dto.CustomerName;
            invoice.EventName = dto.EventName;
            invoice.Amount = dto.Amount;
            invoice.IssuedDate = dto.IssuedDate;
            invoice.DueDate = dto.DueDate;
            invoice.Status = dto.Status;


            await _repository.SaveChangesAsync();

            return new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = invoice.CustomerName,
                EventName = invoice.EventName,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                IssuedDate = invoice.IssuedDate,
                Status = invoice.Status
            };
        }



        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id)) return false;

            await _repository.DeleteAsync(id);
            return true;
        }



    }
}
