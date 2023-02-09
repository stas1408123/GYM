﻿using GYM.API.Data;
using GYM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouchesController : ControllerBase
    {
        private readonly GymDbContext _context;

        public CouchesController(GymDbContext context)
        {
            _context = context;
        }

        // GET: api/Couches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouchViewModel>>> GetCouches()
        {
            return await _context.Couches.ToListAsync();
        }

        // GET: api/Couches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CouchViewModel>> GetCouch(int id)
        {
            var couch = await _context.Couches.FindAsync(id);

            if (couch == null)
            {
                return NotFound();
            }

            return couch;
        }

        // PUT: api/Couches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouch(int id, CouchViewModel couch)
        {
            if (id != couch.Id)
            {
                return BadRequest();
            }

            _context.Entry(couch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouchExists(id))
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

        // POST: api/Couches
        [HttpPost]
        public async Task<ActionResult<CouchViewModel>> PostCouch(CouchViewModel couch)
        {
            _context.Couches.Add(couch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCouch), new { id = couch.Id }, couch);
        }

        // DELETE: api/Couches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouch(int id)
        {
            var couch = await _context.Couches.FindAsync(id);
            if (couch == null)
            {
                return NotFound();
            }

            _context.Couches.Remove(couch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CouchExists(int id)
        {
            return _context.Couches.Any(e => e.Id == id);
        }
    }
}
