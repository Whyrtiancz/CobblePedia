namespace CobblePedia.Models
{
    using System.Threading.Tasks;

    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Sprite
    {
        private static readonly HttpClient Http = new HttpClient();

        public static string FileName { get; } = "sprites.json";

        public static string BasePath { get; } = @"PokeApi\Forms";

        public string BackDefault { get; set; }
        public string BackFemale { get; set; }
        public string BackShiny { get; set; }
        public string BackFemaleShiny { get; set; }
        public string FrontDefault { get; set; }
        public string FrontFemale { get; set; }
        public string FrontShiny { get; set; }
        public string FrontFemaleShiny { get; set; }
        public int SpriteId { get; set; }
        public string Name { get; set; }

        public Sprite()
        {
            Name = "None";
            SpriteId = 0;

            FrontDefault = "0_none_front_default.png";
        }


        public Sprite(JObject source)
        {
            SpriteId = JsonHelper.GetIntValue(source, "id");
            Name = JsonHelper.GetStringValue(source, "name");

            BackDefault = DownloadSprite((string)source.SelectToken("sprites.back_default"), "back_default");
            BackShiny = DownloadSprite((string)source.SelectToken("$.sprites.back_shiny"), "back_default_shiny");
            BackFemale = DownloadSprite((string)source.SelectToken("$.sprites.back_female"), "back_default_female");
            BackFemaleShiny = DownloadSprite((string)source.SelectToken("$.sprites.shiny_female"), "back_default_female_shiny");

            FrontDefault = DownloadSprite((string)source.SelectToken("$.sprites.front_default"), "front_default");
            FrontShiny = DownloadSprite((string)source.SelectToken("$.sprites.front_shiny"), "front_default_shiny");
            FrontFemale = DownloadSprite((string)source.SelectToken("$.sprites.front_female"), "front_default_female");
            FrontFemaleShiny = DownloadSprite((string)source.SelectToken("$.sprites.shiny_female"), "front_default_female_shiny");
        }

        private static async Task SaveImageAsync(string url, string filePath)
        {
            byte[] data = await Http.GetByteArrayAsync(url);
            File.WriteAllBytes(filePath, data);
        }

        private string DownloadSprite(string url, string suffix)
        {
            if (null == url) { return null; }

            string fileName = string.Format("{0}_{1}_{2}.png", SpriteId, Name, suffix);
            string path = Path.Combine(ConfigurationHelper.CobblePediaSpritesPath, fileName);
            if (!File.Exists(path))
            {
                Task.Run(async () =>
                {
                    await SaveImageAsync(url, path);
                });
            }

            return fileName;
        }
    }
}
