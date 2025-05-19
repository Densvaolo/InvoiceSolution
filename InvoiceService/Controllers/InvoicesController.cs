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


       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }


        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutInvoice(int id, UpdateInvoiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _invoiceService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InvoiceDto>> PostInvoice(CreateInvoiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _invoiceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetInvoice), new { id = created.Id }, created);
        }




        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var success = await _invoiceService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

    }
}
