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

        internal Ability() { }

        internal Ability(JObject source)
        {
            AbilityId = JsonHelper.GetStringValue(source, "name").Replace("-", string.Empty);

            CobblePedia.Pedia.FR.Add(KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
            CobblePedia.Pedia.EN.Add(KeyName, JsonHelper.GetTranslation(source, "en", "name"));
            CobblePedia.Pedia.FR.Add(KeyEffect, JsonHelper.GetEffectEntry(source, "fr"));
            CobblePedia.Pedia.EN.Add(KeyEffect, JsonHelper.GetEffectEntry(source, "en"));
            CobblePedia.Pedia.FR.Add(KeyFlavor, JsonHelper.GetFlavorEntry(source, "fr"));
            CobblePedia.Pedia.EN.Add(KeyFlavor, JsonHelper.GetFlavorEntry(source, "en"));
        }
    }
}
