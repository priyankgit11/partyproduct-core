﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyProductCore.Models;
using PartyProductCore.DTO;
using AutoMapper;

namespace PartyProductCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public PartyProductCoreDbContext context { get; set; }
        private readonly IMapper mapper;

        public ProductController(PartyProductCoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<List<ProductDTO>> Get()
        {
            var products = await context.TblProducts.ToListAsync();
            var productsDTO = mapper.Map<List<ProductDTO>>(products);
            return productsDTO ;
        }
        [HttpGet("{id:int}", Name = "getProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var products = await context.TblProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (products == null) return NoContent();
            var productsDTO = mapper.Map<ProductDTO>(products);
            return Ok(productsDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductCreationDTO productCreation)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = mapper.Map<TblProduct>(productCreation);
            context.Add(product);
            await context.SaveChangesAsync();
            var productDTO = mapper.Map<PartyDTO>(product);
            return new CreatedAtRouteResult("getParty", new {Id = productDTO.Id},productDTO);
        }
        [HttpDelete("${id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var products = await context.TblProducts.FirstOrDefaultAsync(i => i.Id == id);
            if (products == null) return NotFound();
            context.TblProducts.Remove(products);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("${id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductCreationDTO productCreation)
        {
            var product = mapper.Map<TblProduct>(productCreation);
            product.Id = id;
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
