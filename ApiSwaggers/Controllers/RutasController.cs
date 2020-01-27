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
    public class RutasController : ControllerBase
    {
        private readonly Api _context;

        public RutasController(Api context)
        {
            _context = context;
        }

        // GET: api/Rutas
        [HttpGet]
        [EnableQuery]
        public IEnumerable<Ruta> GetRuta()
        {
            return _context.ruta;
        }

        // GET: api/Rutas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRuta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ruta = await _context.ruta.FindAsync(id);

            if (ruta == null)
            {
                return NotFound();
            }

            return Ok(ruta);
        }

        // PUT: api/Rutas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuta([FromRoute] int id, [FromBody] Ruta ruta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ruta.id_ruta)
            {
                return BadRequest();
            }

            _context.Entry(ruta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RutaExists(id))
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

        // POST: api/Rutas
        [HttpPost]
        public async Task<IActionResult> PostRuta([FromBody] Ruta ruta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ruta.Add(ruta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuta", new { id = ruta.id_ruta }, ruta);
        }

        // DELETE: api/Rutas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ruta = await _context.ruta.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }

            _context.ruta.Remove(ruta);
            await _context.SaveChangesAsync();

            return Ok(ruta);
        }

        private bool RutaExists(int id)
        {
            return _context.ruta.Any(e => e.id_ruta == id);
        }
    }
}