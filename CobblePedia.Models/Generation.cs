namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Generation
    {
        public static string FileName { get; } = "generations.json";
        public static string BasePath { get; } = @"PokeApi\Generations";

        public string GenerationId { get; set; }
        public string Region { get; set; }

        public string KeyName
        {
            get
            {
                return string.Format(Properties.Resources.GenerationKeyName, GenerationId);
            }
        }

        internal Generation() { }

        internal Generation(JObject source)
        {
            GenerationId = string.Format(Properties.Resources.GenerationId, JsonHelper.GetStringValue(source, "id"));
            Region = JsonHelper.GetStringValue(source, "main_region", "main_region.name");

            CobblePedia.Pedia.FR.Add(KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
            CobblePedia.Pedia.EN.Add(KeyName, JsonHelper.GetTranslation(source, "en", "name"));
        }
    }
}
