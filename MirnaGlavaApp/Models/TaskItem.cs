using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MirnaGlavaApp.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; } // rok zadatka
        public bool IsCompleted { get; set; }

        public RepeatType RepeatType { get; set; } = RepeatType.None;
        public string? RepeatDays { get; set; } // npr. "Monday,Wednesday"
        public int? MonthlyDay { get; set; }

        public int? TaskListId { get; set; }

        [ForeignKey(nameof(TaskListId))]
        public TaskList? TaskList { get; set; }
    }
}
