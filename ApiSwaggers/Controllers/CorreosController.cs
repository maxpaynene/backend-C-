using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSwaggers.ApiContext;
using ApiSwaggers.Models;
using Microsoft.AspNet.OData;
using System;

namespace ApiSwaggers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery(EnableCorrelatedSubqueryBuffering = true)]
    public class CorreosController : ControllerBase
    {
        private readonly Api _context;

        public CorreosController(Api context)
        {
            _context = context;
        }
        
        // GET: api/Correos
        [HttpGet]
        [EnableQuery(EnableCorrelatedSubqueryBuffering = true)]
        public IQueryable GetCorreo()
        {
            return _context.correo.
                Join(_context.cliente, co => co.id_cliente, cl => cl.id_cliente, (co, cl) => new { cliente = cl, correo = co })
                .Select(x => new CorreoJoinCliente {
                    cliente = x.cliente,
                    id_correo = x.correo.id_correo,
                    correo =  x.correo.correo,
                    fecha_ingreso = x.correo.fecha_ingreso,
                    id_cliente = x.correo.id_cliente
                });
        }

        // GET: api/Correos/5
        [HttpGet("{id}")]
        [EnableQuery()]
        public async Task<IActionResult> GetCorreo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var correo = await _context.correo.FindAsync(id);

            if (correo == null)
            {
                return NotFound();
            }

            return Ok(correo);
        }

        // PUT: api/Correos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorreo([FromRoute] int id, [FromBody] Correo correo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != correo.id_correo)
            {
                return BadRequest();
            }

            _context.Entry(correo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorreoExists(id))
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

        // POST: api/Correos
        [HttpPost]
        public async Task<IActionResult> PostCorreo([FromBody] Correo correo)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            correo.fecha_ingreso = DateTime.Now;

            _context.correo.Add(correo);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetCorreo", new { id = correo.id_correo }, correo);
        }

        // DELETE: api/Correos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorreo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var correo = await _context.correo.FindAsync(id);
            if (correo == null)
            {
                return NotFound();
            }

            _context.correo.Remove(correo);
            await _context.SaveChangesAsync();

            return Ok(correo);
        }

        private bool CorreoExists(int id)
        {
            return _context.correo.Any(e => e.id_correo == id);
        }
    }
}