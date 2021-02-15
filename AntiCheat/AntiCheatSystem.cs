using Avalonia.Controls;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

#pragma warning disable 8618

namespace AntiCheat
{
    public class AntiCheatSystem
    {
        ProcessStartInfo info;

        /*public void GetDirsInRootDir(string[] files) => 
            Config.ListDir = Directory.GetDirectories(Path.GetDirectoryName(files[0]));*/

        public bool CheckAllFiles() 
        {
            bool status = false;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                for (int i = 0; i < Config.ListDirInRootDir.Length; i++)
                {
                    if (Directory.Exists(Config.PathToMinecraft + "/" + Config.ListDirInRootDir[i])) status = true;
                    else status = false;
                }
            }
            else if ((RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) || (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)))
                if (Directory.Exists(Config.PathToMinecraft)) status = true;
                else status = false;

            return status;
        }

        public void KillMinecraftProcess() 
        { 
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                System.Diagnostics.Process.Start("CMD.exe", "/C taskkill /F /IM javaw.exe /T");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                System.Diagnostics.Process.Start("/bin/bash", "-c 'killall Java'");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                System.Diagnostics.Process.Start("/bin/bash", "-c 'killall Java'");
        }
        public void StartMinecraftProcess() 
        {
            if (File.Exists(Path.Combine(Config.PathToMinecraft, Config.MinecraftFileName)))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    if (Config.MinecraftFileName.EndsWith(".exe"))
                        Process.Start(Config.FullMinecraftPath);
                    else if (Config.MinecraftFileName.EndsWith(".jar"))
                        Process.Start(Config.FullMinecraftPath);
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        Process.Start(Path.Combine(Config.MinecraftFileName));
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        Process.Start(Config.MinecraftFileName);
            }
            else new DialogWindow("Файл " + Config.MinecraftFileName + " не найден", "Ошибка").Show();
        }

        public bool SearchCheat() 
        {
            bool status = false;

            if (Directory.Exists(Path.Combine(Config.PathToMinecraft, "mods")))
                if ((Directory.GetDirectories(Path.Combine(Config.PathToMinecraft, "mods")).Length > 0) || (Directory.GetFiles(Path.Combine(Config.PathToMinecraft, "mods")).Length > 0))
                    status = true;

            return status;
        }

        public void RunSearches() 
        {
            StartMinecraftProcess();

            while (true)
            {
                if (SearchCheat()) 
                {
                    KillMinecraftProcess();
                    break; 
                }
                else continue;
            }
        }
    }
}
