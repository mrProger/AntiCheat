using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AntiCheat
{
    public class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public DialogWindow(string Message, string Header) 
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.FindControl<TextBlock>("MessageBlock").Text = Message;
            this.Title = Header;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OkButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e) => this.Hide();
    }
}
