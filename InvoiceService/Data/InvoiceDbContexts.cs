using Microsoft.EntityFrameworkCore;
using InvoiceService.Models;

namespace InvoiceService.Data
{
    public class InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : DbContext(options)
    {
        public DbSet<Invoice> Invoices { get; set; }
    }
}



