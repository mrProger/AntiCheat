using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AntiCheat
{
    public class SetMinecraftName : Window
    {
        public SetMinecraftName()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ConfirmNameButtonClick(object sender, RoutedEventArgs e) 
        {
            if ((!this.FindControl<TextBox>("ClientNameBox").Text.Equals("")) && (this.FindControl<TextBox>("ClientNameBox").Text.EndsWith(".jar")))
            { 
                Config.MinecraftFileName = this.FindControl<TextBox>("ClientNameBox").Text;
                Close();
            }
            else new DialogWindow("Файл не найден", "Ошибка").Show();
        }
    }
}
