using MirnaGlavaApp.Models;

namespace MirnaGlavaApp.Views
{
    public partial class MainFlyoutPage : ContentPage
    {
        public MainFlyoutPage()
        {
            InitializeComponent();
        }

        private async void OnListSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is TaskList selectedList)
            {
                await Navigation.PushAsync(new TaskListPage(selectedList.Id, selectedList.Name));
            }
        }
    }
}
