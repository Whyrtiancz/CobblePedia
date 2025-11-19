namespace CobblePedia.Builder.Jar
{
    using CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    internal sealed class Minecraft
    {
        private static readonly string[] Paths =
        {
            "assets/minecraft/textures/block",
            "assets/minecraft/textures/item",
        };

        private string root;
        public JObject FR { get; private set; }
        public JObject EN { get; private set; }

        public Minecraft(string jarPath)
        {
            root = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "Minecraft");
            JarHelper.ExtractSubfolder(jarPath, root, Minecraft.Paths, true);

            LoadLanguages();
        }

        private void LoadLanguages()
        {
            Console.WriteLine("Minecraft Load languages");

            string filePath = Path.Combine(root, "fr_fr.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                FR = JObject.Parse(jsonContent);
            }

            filePath = Path.Combine(root, "en_us.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                EN = JObject.Parse(jsonContent);
            }
        }
    }
}
