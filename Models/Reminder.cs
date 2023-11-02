using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteWeb.API.Models
{
    public class Reminder
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TimeReminder { get; set; }

        public Guid NoteId { get; set; }
        public Note? Note { get; set; }
    }
}
