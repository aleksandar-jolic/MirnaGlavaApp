using MirnaGlavaApp.Models;
using MirnaGlavaApp.ViewModels;

namespace MirnaGlavaApp.Views
{
    public partial class TodayPage : ContentPage
    {
        public TodayPage()
        {
            InitializeComponent();
        }

        private async void OnTaskCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (BindingContext is MainViewModel vm &&
                sender is CheckBox checkBox &&
                checkBox.BindingContext is TaskItem task)
            {
                await vm.ToggleTaskAsync(task);
            }
        }
    }
}
