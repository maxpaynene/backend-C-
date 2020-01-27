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
    public class ClavesController : ControllerBase
    {
        private readonly Api _context;

        public ClavesController(Api context)
        {
            _context = context;
        }

        // GET: api/Claves
        [HttpGet]
        [EnableQuery]
        public IEnumerable<Clave> GetClave()
        {
            return _context.clave;
        }

        // GET: api/Claves/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClave([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clave = await _context.clave.FindAsync(id);

            if (clave == null)
            {
                return NotFound();
            }

            return Ok(clave);
        }

        // PUT: api/Claves/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClave([FromRoute] int id, [FromBody] Clave clave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clave.id_clave)
            {
                return BadRequest();
            }

            _context.Entry(clave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaveExists(id))
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

        // POST: api/Claves
        [HttpPost]
        public async Task<IActionResult> PostClave([FromBody] Clave clave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.clave.Add(clave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClave", new { id = clave.id_clave }, clave);
        }

        // DELETE: api/Claves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClave([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clave = await _context.clave.FindAsync(id);
            if (clave == null)
            {
                return NotFound();
            }

            _context.clave.Remove(clave);
            await _context.SaveChangesAsync();

            return Ok(clave);
        }

        private bool ClaveExists(int id)
        {
            return _context.clave.Any(e => e.id_clave == id);
        }
    }
}