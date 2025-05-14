using InvoiceService.Data;
using InvoiceService.Dtos;
using InvoiceService.Services;
using InvoiceService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceDbContext _context;
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(InvoiceDbContext context, IInvoiceService invoiceService)
        {
            _context = context;
            _invoiceService = invoiceService;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, UpdateInvoiceDto dto)
        {
            var updated = await _invoiceService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // Använda modelstate?
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(CreateInvoiceDto dto)
        {
            var created = await _invoiceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetInvoice), new { id = created.Id }, created);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var success = await _invoiceService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }


        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
