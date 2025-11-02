namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Nature
    {
        public static string FileName { get; } = "natures.json";
        public static string BasePath { get; } = @"PokeApi\Natures";

        public string NatureId { get; set; }
        public string DecreasedStat { get; set; }
        public string HatesFlavor { get; set; }
        public string IncreasedStat { get; set; }
        public string LikesFlavor { get; set; }

        public string KeyName
        {
            get
            {
                return string.Format("Nature_{0}", NatureId);
            }
        }

        public Nature() { }

        internal Nature(JObject source)
        {
            NatureId = JsonHelper.GetStringValue(source, "name");
            CobblePedia.Pedia.FR.Add(KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
            CobblePedia.Pedia.EN.Add(KeyName, JsonHelper.GetTranslation(source, "en", "name"));

            DecreasedStat = JsonHelper.GetStringValue(source, "decreased_stat", "decreased_stat.name");
            HatesFlavor = JsonHelper.GetStringValue(source, "hates_flavor", "hates_flavor.name");
            IncreasedStat = JsonHelper.GetStringValue(source, "increased_stat", "increased_stat.name");
            LikesFlavor = JsonHelper.GetStringValue(source, "likes_flavor", "likes_flavor.name");
        }
    }

}
