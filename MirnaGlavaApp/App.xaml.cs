namespace MirnaGlavaApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // pokreće tvoju glavnu stranicu
        return new Window(new Views.MainFlyoutPage());

    }
}
