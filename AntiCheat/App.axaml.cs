using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace AntiCheat
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.MainWindow = new MainWindow();

            else if (ApplicationLifetime is IControlledApplicationLifetime applicationLifetime)
                applicationLifetime.Exit += (s, a) => { new AntiCheatSystem().KillMinecraftProcess(); };

            base.OnFrameworkInitializationCompleted();
        }
    }
}
