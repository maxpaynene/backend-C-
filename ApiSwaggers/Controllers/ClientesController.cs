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
    public class ClientesController : ControllerBase
    {
        private readonly Api _context;

        public ClientesController(Api context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        [EnableQuery(EnableCorrelatedSubqueryBuffering = true)]
        public IQueryable GetCliente()
        {
            return _context.cliente;
            //return _context.cliente.Join(_context.correo, cl => cl.id_cliente, co => co.id_cliente, (cl, co) => new { cliente = cl, correo = co });
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        [EnableQuery(EnableCorrelatedSubqueryBuffering = true)]
        public async Task<IActionResult> GetCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente([FromRoute] int id, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.id_cliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cliente.fecha_ingreso = DateTime.Now;

            _context.cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.id_cliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        private bool ClienteExists(int id)
        {
            return _context.cliente.Any(e => e.id_cliente == id);
        }
    }
}