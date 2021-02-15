using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCheat
{
    public static class Config
    {
        public static string PathToMinecraft = "";
        public static string MinecraftFileName = "";
        public static string FullMinecraftPath = "";
        public static string[] ListDirInRootDir = new string[] 
        {
            "assets", "libraries", "logs", "mods", "server-resource-packs", "versions"
        };
    }
}
