using Microsoft.Extensions.Logging;
using MirnaGlavaApp.Data;
using MirnaGlavaApp.Services;

namespace MirnaGlavaApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 🔹 1️⃣ Putanja do lokalne baze
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db3");

            // 🔹 2️⃣ Registruj bazu i servise kao Singleton (jedan primer kroz ceo app)
            builder.Services.AddSingleton<AppDbContext>(s => new AppDbContext(dbPath));
            builder.Services.AddSingleton<TaskService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
