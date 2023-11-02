using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using NoteWeb.API.Models;

namespace NoteWeb.API.Data
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Tag> dbTags { get; set; }

        public virtual DbSet<Note> dbNotes { get; set; }

        public virtual DbSet<Reminder> dbReminder { get; set; }
    }
}
