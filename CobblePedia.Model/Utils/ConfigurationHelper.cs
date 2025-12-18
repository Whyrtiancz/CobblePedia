namespace CobblePedia.Models.Utils
{
    using System;

    public static class ConfigurationHelper
    {
        public static string CobblePediaDataPath
        {
            get
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
                string path = Path.Combine(localAppData, "CobblePedia");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
        public static string CobblePediaSpritesPath
        {
            get
            {
                string path = Path.Combine(CobblePediaDataPath, "Sprites");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
    }
}