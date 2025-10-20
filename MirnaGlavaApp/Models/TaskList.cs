using System.ComponentModel.DataAnnotations;

namespace MirnaGlavaApp.Models
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
