using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcariPlanillaAPI.Context;
using AcariPlanillaAPI.Models;

namespace AcariPlanillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletasController(PlanillaDbContext context) : ControllerBase
    {
        private readonly PlanillaDbContext _context = context;

        // GET: api/Boletas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boletas>>> GetBoletas()
        {
            return await _context.Boletas.ToListAsync();
        }

        // GET: api/Boletas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Boletas>> GetBoletas(int id)
        {
            var boletas = await _context.Boletas.FindAsync(id);

            if (boletas == null)
            {
                return NotFound();
            }

            return boletas;
        }

        // PUT: api/Boletas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoletas(int id, Boletas boletas)
        {
            if (id != boletas.BoletaId)
            {
                return BadRequest();
            }

            _context.Entry(boletas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoletasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Boletas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Boletas>> PostBoletas(Boletas boletas)
        {
            _context.Boletas.Add(boletas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoletas", new { id = boletas.BoletaId }, boletas);
        }

        // DELETE: api/Boletas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoletas(int id)
        {
            var boletas = await _context.Boletas.FindAsync(id);
            if (boletas == null)
            {
                return NotFound();
            }

            _context.Boletas.Remove(boletas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoletasExists(int id)
        {
            return _context.Boletas.Any(e => e.BoletaId == id);
        }
    }
}
