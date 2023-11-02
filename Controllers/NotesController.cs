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
    public class NotesController : ControllerBase
    {
        private readonly NoteDbContext _context;

        public NotesController(NoteDbContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetdbNotes()
        {
            if (_context.dbNotes == null)
            {
                return NotFound();
            }

            var listNote = await _context.dbNotes.ToListAsync();

            //var noteTimeReminder = await _context.dbNotes.Include(n => n.NoteReminder).ToListAsync();
            //Console.WriteLine(noteTimeReminder.ToString());

            foreach (var note in listNote)
            {
                note.Tags = await _context.dbTags.FindAsync(note.TagsId);

                //(await _context.dbReminder.FirstOrDefaultAsync(x => x.NoteId == note.Id))
                var timeNoteReminder = await _context.dbReminder.FirstOrDefaultAsync(x => x.NoteId == note.Id);

                if (timeNoteReminder != null)
                {
                    note.NoteReminder = timeNoteReminder.TimeReminder;
                }
            }


            return listNote;
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(Guid id)
        {
          if (_context.dbNotes == null)
          {
              return NotFound();
          }
            var note = await _context.dbNotes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(Guid id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            if (_context.dbNotes == null)
            {
                return Problem("Entity set 'NoteDbContext.dbNotes'  is null.");
            }



            _context.dbNotes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            if (_context.dbNotes == null)
            {
                return NotFound();
            }
            var note = await _context.dbNotes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.dbNotes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(Guid id)
        {
            return (_context.dbNotes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
