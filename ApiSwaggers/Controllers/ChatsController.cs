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
    public class ChatsController : ControllerBase
    {
        private readonly Api _context;

        public ChatsController(Api context)
        {
            _context = context;
        }

        // GET: api/Chats
        [HttpGet]
        [EnableQuery]
        public IEnumerable<Chat> GetChat()
        {
            return _context.chat;
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chat = await _context.chat.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }

        // PUT: api/Chats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat([FromRoute] int id, [FromBody] Chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chat.id_chat)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
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

        // POST: api/Chats
        [HttpPost]
        public async Task<IActionResult> PostChat([FromBody] Chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.chat.Add(chat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChat", new { id = chat.id_chat }, chat);
        }

        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chat = await _context.chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.chat.Remove(chat);
            await _context.SaveChangesAsync();

            return Ok(chat);
        }

        private bool ChatExists(int id)
        {
            return _context.chat.Any(e => e.id_chat == id);
        }
    }
}