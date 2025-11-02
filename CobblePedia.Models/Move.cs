namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Move
    {
        public static string FileName { get; } = "moves.json";
        public static string BasePath { get; } = @"PokeApi\Moves";

        public string MoveId { get; set; }
        public string DamageClass { get; set; }
        public string Target { get; set; }
        public string MoveType { get; set; }

        public string KeyName
        {
            get
            {
                return string.Format(Properties.Resources.MoveKeyName, MoveId);
            }
        }

        public string KeyEffect
        {
            get
            {
                return string.Format(Properties.Resources.MoveKeyEffect, MoveId);
            }
        }

        public string KeyFlavor
        {
            get
            {
                return string.Format(Properties.Resources.MoveKeyFlavor, MoveId);
            }
        }

        internal Move() { }

        internal Move(JObject source)
        {
            MoveId = JsonHelper.GetStringValue(source, "name").Replace("-", string.Empty);

            CobblePedia.Pedia.FR.Add(KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
            CobblePedia.Pedia.EN.Add(KeyName, JsonHelper.GetTranslation(source, "en", "name"));
            CobblePedia.Pedia.FR.Add(KeyEffect, JsonHelper.GetEffectEntry(source, "fr"));
            CobblePedia.Pedia.EN.Add(KeyEffect, JsonHelper.GetEffectEntry(source, "en"));
            CobblePedia.Pedia.FR.Add(KeyFlavor, JsonHelper.GetFlavorEntry(source, "fr"));
            CobblePedia.Pedia.EN.Add(KeyFlavor, JsonHelper.GetFlavorEntry(source, "en"));

            DamageClass = JsonHelper.GetStringValue(source, "damage_class", "damage_class.name");
            Target = JsonHelper.GetStringValue(source, "target", "target.name");
            MoveType = JsonHelper.GetStringValue(source, "type", "type.name");
        }
    }
}
