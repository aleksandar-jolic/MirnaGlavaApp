using MirnaGlavaApp.Models;
using MirnaGlavaApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MirnaGlavaApp.ViewModels
{
    public class MainViewModel
    {
        private readonly TaskService _taskService;

        // 🔹 Observable kolekcije za UI binding
        public ObservableCollection<TaskList> TaskLists { get; set; } = new();
        public ObservableCollection<TaskItem> TodayTasks { get; set; } = new();

        // 🔹 Komande
        public ICommand AddListCommand { get; }
        public ICommand OpenTodayCommand { get; }
        public ICommand OpenWeekCommand { get; }
        public ICommand OpenMonthCommand { get; }

        public MainViewModel(TaskService taskService)
        {
            _taskService = taskService;

            // Inicijalizacija komandi
            AddListCommand = new Command(async () => await AddListAsync());
            OpenTodayCommand = new Command(async () => await OpenTodayAsync());
            OpenWeekCommand = new Command(async () => await OpenWeekAsync());
            OpenMonthCommand = new Command(async () => await OpenMonthAsync());

            _ = LoadDataAsync();
        }

        // 🔹 Prazan konstruktor za XAML dizajn (preview)
        public MainViewModel()
        {
        }

        // 🔹 Učitavanje svih listi i dnevnih taskova
        private async Task LoadDataAsync()
        {
            var lists = await _taskService.GetAllListsAsync();
            TaskLists.Clear();

            foreach (var list in lists)
                TaskLists.Add(list);

            // Automatski učitaj i "današnje" taskove
            var todayTasks = await _taskService.GetTodayTasksAsync();
            TodayTasks.Clear();

            foreach (var task in todayTasks)
                TodayTasks.Add(task);
        }

        // 🔹 Dodavanje nove liste
        private async Task AddListAsync()
        {
            string result = await Application.Current.MainPage.DisplayPromptAsync(
                "Nova lista",
                "Unesi ime liste:"
            );

            if (!string.IsNullOrWhiteSpace(result))
            {
                await _taskService.AddListAsync(result);
                await LoadDataAsync();
            }
        }

        // 🔹 Navigacija na “Danas”, “Nedelja”, “Mesec”
        private async Task OpenTodayAsync()
        {
            await Shell.Current.GoToAsync("//TodayPage");
        }

        private async Task OpenWeekAsync()
        {
            await Shell.Current.GoToAsync("//WeekPage");
        }

        private async Task OpenMonthAsync()
        {
            await Shell.Current.GoToAsync("//MonthPage");
        }
    }
}
