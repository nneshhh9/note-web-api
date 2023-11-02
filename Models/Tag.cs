using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoteWeb.API.Models
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? TagName { get; set; }
    }
}
