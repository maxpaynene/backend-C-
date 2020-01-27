using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSwaggers.ApiContext;
using ApiSwaggers.Models;
using Microsoft.AspNet.OData;

namespace ApiSwaggers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregasController : ControllerBase
    {
        private readonly Api _context;

        public EntregasController(Api context)
        {
            _context = context;
        }

        // GET: api/Entregas
        [HttpGet]
        [EnableQuery]
        public IEnumerable<Entrega> GetEntrega()
        {
            return _context.entrega;
        }

        // GET: api/Entregas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntrega([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entrega = await _context.entrega.FindAsync(id);

            if (entrega == null)
            {
                return NotFound();
            }

            return Ok(entrega);
        }

        // PUT: api/Entregas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrega([FromRoute] int id, [FromBody] Entrega entrega)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entrega.id_entrega)
            {
                return BadRequest();
            }

            _context.Entry(entrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
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

        // POST: api/Entregas
        [HttpPost]
        public async Task<IActionResult> PostEntrega([FromBody] Entrega entrega)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.entrega.Add(entrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrega", new { id = entrega.id_entrega }, entrega);
        }

        // DELETE: api/Entregas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entrega = await _context.entrega.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }

            _context.entrega.Remove(entrega);
            await _context.SaveChangesAsync();

            return Ok(entrega);
        }

        private bool EntregaExists(int id)
        {
            return _context.entrega.Any(e => e.id_entrega == id);
        }
    }
}