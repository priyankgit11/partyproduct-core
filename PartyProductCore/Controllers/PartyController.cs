﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyProductCore.Models;
using PartyProductCore.DTO;

namespace PartyProductCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        public PartyProductCoreDbContext context { get; set; }
        private readonly IMapper mapper;

        public PartyController(PartyProductCoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<List<PartyDTO>> Get()
        {
            var parties = await context.TblParties.ToListAsync();
            var partiesDTO = mapper.Map<List<PartyDTO>>(parties);
            return partiesDTO;
        }
        [HttpGet("{id:int}", Name = "getParty")]
        public async Task<ActionResult<PartyDTO>> Get(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var party = await context.TblParties.FirstOrDefaultAsync(x => x.Id == id);
            if (party == null) return NoContent();
            var partyDTO = mapper.Map<PartyDTO>(party);
            return Ok(partyDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PartyCreationDTO partyCreation)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var party = mapper.Map<TblParty>(partyCreation);
            context.Add(party);
            await context.SaveChangesAsync();
            var partyDTO = mapper.Map<PartyDTO>(party);
            return new CreatedAtRouteResult("getParty", new {Id = partyDTO.Id},partyDTO);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var party = await context.TblParties.FirstOrDefaultAsync(i => i.Id == id);
            if (party == null) return NotFound();
            context.TblParties.Remove(party);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("${id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PartyCreationDTO partyCreation)
        {
            var party = mapper.Map<TblParty>(partyCreation);
            party.Id = id;
            context.Entry(party).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
