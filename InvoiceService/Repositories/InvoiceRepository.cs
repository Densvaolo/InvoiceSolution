using InvoiceService.Data;
using InvoiceService.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Repositories
{
    public class InvoiceRepository(InvoiceDbContext context) : IInvoiceRepository
    {
        private readonly InvoiceDbContext _context = context;

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices.FindAsync(id);
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Invoices.AnyAsync(e => e.Id == id);
        }

       
    }
}
