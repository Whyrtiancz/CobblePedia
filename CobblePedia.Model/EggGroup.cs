namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class EggGroup
    {
        public static string FileName { get; } = "egggroups.json";
        public static string BasePath { get; } = @"PokeApi\Egg-Groups";

        public string EggGroupId { get; set; }

        public string KeyName
        {
            get
            {
                return string.Format(Properties.Resources.EggGroupKeyName, EggGroupId);
            }
        }

        public EggGroup() { }

        public EggGroup(JObject source)
        {
            EggGroupId = JsonHelper.GetStringValue(source, "name");
        }
    }
}