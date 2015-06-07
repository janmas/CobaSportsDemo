using System.ComponentModel.DataAnnotations;

namespace AngularMinidemo.Models
{
    public class Sport
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Caption { get; set; }
    }
}