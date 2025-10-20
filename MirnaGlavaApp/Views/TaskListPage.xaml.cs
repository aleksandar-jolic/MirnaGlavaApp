using MirnaGlavaApp.Data;
using MirnaGlavaApp.Services;

namespace MirnaGlavaApp.Views;

public partial class TaskListPage : ContentPage
{
    private readonly TaskService _taskService;
    private readonly int _listId;

    public TaskListPage(int listId, string listName)
    {
        InitializeComponent();
        _listId = listId;
        ListTitle.Text = listName;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db3");
        var db = new AppDbContext(dbPath);
        _taskService = new TaskService(db);

        LoadTasks();
    }

    private async void LoadTasks()
    {
        var tasks = await _taskService.GetTasksByListIdAsync(_listId);
        TasksView.ItemsSource = tasks;
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Novi zadatak", "Unesi naziv:");
        if (!string.IsNullOrWhiteSpace(result))
        {
            await _taskService.AddTaskAsync(_listId, result);
            LoadTasks();
        }
    }
}
