using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteWeb.API.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string? NoteTitle { get; set; }

        [StringLength(1000)]
        public string? NoteContext { get; set; }

        public DateTime NoteReminder { get; set; }

        //navigation properties
        public Guid TagsId { get; set; }
        public Tag? Tags { get; set; }



    }
}
