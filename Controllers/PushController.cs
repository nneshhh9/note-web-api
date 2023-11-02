using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteWeb.API.Data;
using NoteWeb.API.Models;

namespace NoteWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushController : Controller
    {
        private readonly NoteDbContext _context;

        public PushController(NoteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reminder>>> GetPush()
        {
            if (_context.dbReminder == null)
            {
                return NotFound();
            }

            var listReminder = await _context.dbReminder.ToListAsync();


            var notifications = new List<Reminder>();



            foreach (var time in listReminder)
            {

                if (time.TimeReminder <= (DateTime.Now))
                {
                    time.Note = await _context.dbNotes.FindAsync(time.NoteId);
                    notifications.Add(time);
                }
            }

            return notifications;
        }
    }
}
