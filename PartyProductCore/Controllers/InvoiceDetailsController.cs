using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyProductCore.DTO;
using PartyProductCore.Models;

namespace PartyProductCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailsController : ControllerBase
    {
        public PartyProductCoreDbContext context { get; }
        private readonly IMapper mapper;
        public InvoiceDetailsController(PartyProductCoreDbContext Context, IMapper mapper)
        {
            context = Context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<List<InvoiceDetailsRelationDTO>> Get()
        {
            var invoiceDetails = await context.TblInvoiceDetails.Include(i => i.Party).Include(i => i.Product).ToListAsync();
            var invoiceDetailsDTO = mapper.Map<List<InvoiceDetailsRelationDTO>>(invoiceDetails);
            return invoiceDetailsDTO;
        }
        [HttpGet("{id:int}", Name = "getInvoice")]
        public async Task<ActionResult<InvoiceDetailsRelationDTO>> GetRate(int id)
        {
            var invoice = await context.TblInvoiceDetails.FirstOrDefaultAsync(i => i.Id == id);
            if (invoice == null) return NoContent();
            var invoiceDetailDTO = mapper.Map<ProductRateRelationDTO>(invoice);
            return Ok(invoiceDetailDTO );
        }
        [HttpPost]
        public async Task<ActionResult<InvoiceDetailsRelationDTO>> Post([FromBody] InvoiceDetailCreationDTO invoiceDetailsDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var invoiceDetail = mapper.Map<TblInvoiceDetail>(invoiceDetailsDTO);
            context.TblInvoiceDetails.Add(invoiceDetail);
            await context.SaveChangesAsync();
            var productRateDTO = mapper.Map<InvoiceDetailsRelationDTO>(invoiceDetail);
            return new CreatedAtRouteResult("getInvoice", new { Id = productRateDTO.Id }, productRateDTO);
        }
    }
}
