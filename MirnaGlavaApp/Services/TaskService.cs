using Microsoft.EntityFrameworkCore;
using MirnaGlavaApp.Data;
using MirnaGlavaApp.Models;

namespace MirnaGlavaApp.Services
{
    public class TaskService
    {
        private readonly AppDbContext _db;

        public TaskService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TaskList>> GetListsAsync()
        {
            return await _db.Lists.Include(l => l.Tasks).ToListAsync();
        }

        public async Task AddListAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            var list = new TaskList { Name = name };
            _db.Lists.Add(list);
            await _db.SaveChangesAsync();
        }

        public async Task AddTaskAsync(int listId, string title, DateTime? dueDate = null, RepeatType repeatType = RepeatType.None)
        {
            if (string.IsNullOrWhiteSpace(title)) return;

            var task = new TaskItem
            {
                Title = title,
                IsCompleted = false,
                TaskListId = listId == 0 ? null : listId,
                DueDate = dueDate,
                RepeatType = repeatType
            };
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
        }

        public async Task ToggleTaskAsync(TaskItem task)
        {
            task.IsCompleted = !task.IsCompleted;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TaskItem>> GetTodayTasksAsync()
        {
            var today = DateTime.Today;

            // vraća taskove koji imaju due date danas
            return await _db.Tasks
                .Where(t => t.DueDate != null && t.DueDate.Value.Date == today)
                .Include(t => t.TaskList)
                .ToListAsync();
        }

        // Vraca sve liste sa brojem taskova
        public async Task<List<TaskList>> GetAllListsAsync()
        {
            return await _db.Lists
                .Include(l => l.Tasks)
                .ToListAsync();
        }

        // Vraca taskove za odredjenu listu po ID-u
        public async Task<List<TaskItem>> GetTasksByListIdAsync(int listId)
        {
            return await _db.Tasks
                .Where(t => t.TaskListId == listId)
                .ToListAsync();
        }

    }
}
