using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteWeb.API.Data;
using NoteWeb.API.Models;

namespace NoteWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly NoteDbContext _context;

        public RemindersController(NoteDbContext context)
        {
            _context = context;
        }

        // GET: api/Reminders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reminder>>> GetdbReminder()
        {
            if (_context.dbReminder == null)
            {
                return NotFound();
            }

            var listRem = await _context.dbReminder.ToListAsync();

            foreach (var reminder in listRem)
            {
                reminder.Note = await _context.dbNotes.FindAsync(reminder.NoteId);
            }

            return listRem;
        }

        // GET: api/Reminders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reminder>> GetReminder(Guid id)
        {
          if (_context.dbReminder == null)
          {
              return NotFound();
          }
            var reminder = await _context.dbReminder.FindAsync(id);

            if (reminder == null)
            {
                return NotFound();
            }

            return reminder;
        }

        // PUT: api/Reminders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminder(Guid id, Reminder reminder)
        {
            if (id != reminder.Id)
            {
                return BadRequest();
            }

            _context.Entry(reminder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(id))
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

        // POST: api/Reminders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reminder>> PostReminder(Reminder reminder)
        {
          if (_context.dbReminder == null)
          {
              return Problem("Entity set 'NoteDbContext.dbReminder'  is null.");
          }
            _context.dbReminder.Add(reminder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminder", new { id = reminder.Id }, reminder);
        }

        // DELETE: api/Reminders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(Guid id)
        {
            if (_context.dbReminder == null)
            {
                return NotFound();
            }
            var reminder = await _context.dbReminder.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            _context.dbReminder.Remove(reminder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReminderExists(Guid id)
        {
            return (_context.dbReminder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
