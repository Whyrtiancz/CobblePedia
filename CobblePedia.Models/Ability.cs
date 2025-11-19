namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Ability
    {
        public static string FileName { get; } = "abilities.json";
        public static string BasePath { get; } = @"PokeApi\Abilities";

        public string AbilityId { get; set; }

        public string KeyName
        {
            get
            {
                return string.Format(Properties.Resources.AbilityKeyName, AbilityId);
            }
        }

        public string KeyEffect
        {
            get
            {
                return string.Format(Properties.Resources.AbilityKeyEffect, AbilityId);
            }
        }

        public string KeyFlavor
        {
            get
            {
                return string.Format(Properties.Resources.AbilityKeyFlavor, AbilityId);
            }
        }

        public Ability() { }

        public Ability(JObject source)
        {
            AbilityId = JsonHelper.GetStringValue(source, "name").Replace("-", string.Empty);
        }
    }
}
