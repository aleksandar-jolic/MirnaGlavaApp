namespace MirnaGlavaApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnTaskCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkbox && checkbox.BindingContext is Models.TaskItem task)
        {
            // promena statusa
            task.IsCompleted = e.Value;

            // pozovi servis ili ViewModel da saèuva promenu
            var vm = BindingContext as ViewModels.MainViewModel;
            if (vm != null)
                await vm.ToggleTaskAsync(task);
        }
    }
}