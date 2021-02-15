using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#pragma warning disable 4014
#pragma warning disable 8601
#pragma warning disable 8622
#pragma warning disable 8602

namespace AntiCheat
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            Initialized += AntiCheatInit;
            Activated += AntiCheatStart;

            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public AntiCheatSystem antiCheat = new AntiCheatSystem();

        private void ExitButtonClick(object sender, RoutedEventArgs e) 
        {
            antiCheat.KillMinecraftProcess();
            Close(); 
        }

        private void ChangePathButtonClick(object sender, RoutedEventArgs e) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                GetPathToMinecraftWin();
            else if ((RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) || (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)))
                GetPathToMinecraftUnix();
        }

        private void GetPathToMinecraftUnix()
        {
            new SetMinecraftName().Show();
            for (int i = 0; i < Config.ListDirInRootDir.Length; i++) Config.ListDirInRootDir[i].Remove(i); 
            Config.ListDirInRootDir[0] = "logs";
            Config.PathToMinecraft = "/home/" + Environment.UserName + "/.minecraft/";
            if (Directory.Exists("/home/" + Environment.UserName + "/.minecraft/")) antiCheat.SearchCheat();
        }

        private async Task GetPathToMinecraftWin()
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Minecraft", Extensions = { "jar", "exe" } });
            var files = await dialog.ShowAsync(this);

            Config.PathToMinecraft = Path.GetDirectoryName(files[0]);
            Config.MinecraftFileName = Path.GetFileName(files[0]);
            Config.FullMinecraftPath = Path.GetFullPath(files[0]);

            CheckFiles();
        }

        private async Task CheckFiles() 
        {
            if (antiCheat.CheckAllFiles())
            {
                if (!File.Exists("path.empireac")) using (StreamWriter sw = new StreamWriter("path.empireac", false, System.Text.Encoding.Default))
                    {
                        await sw.WriteLineAsync(Config.FullMinecraftPath);
                    }
                StartWork();
            }
            else
            {
                new DialogWindow("Необходимые файлы не найдены", "Ошибка").Show();
                Config.PathToMinecraft = "";
                Config.MinecraftFileName = "";
                Config.FullMinecraftPath = "";
            }
        }

        private void StartWork() 
        {
            antiCheat.RunSearches();
            new DialogWindow("Обнаружены подозрительные файлы/директории. Пожалуйста, удалите их и попробуйте снова", "Ошибка").Show();
            Close();
        }

        private void AntiCheatInit(object sender, System.EventArgs e) => antiCheat.KillMinecraftProcess();

        private void AntiCheatStart(object sender, System.EventArgs e) 
        { 
            if (File.Exists("path.empireac")) using (StreamReader sr = new StreamReader("path.empireac"))
            {
               Config.FullMinecraftPath = sr.ReadLine();
               sr.Close();
               Config.MinecraftFileName = Path.GetFileName(Config.FullMinecraftPath);
               Config.PathToMinecraft = Path.GetDirectoryName(Config.FullMinecraftPath);
            }

            if (!Config.FullMinecraftPath.Equals("")) CheckFiles();
        }
    }
}