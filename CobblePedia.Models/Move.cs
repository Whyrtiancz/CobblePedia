namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Move
    {
        public static string FileName { get; } = "moves.json";
        public static string BasePath { get; } = @"PokeApi\Moves";

        public int Numero { get; set; }
        public string MoveId { get; set; }
        public string DamageClass { get; set; }
        public string Target { get; set; }
        public string MoveType { get; set; }
        public int Accuracy { get; set; }
        public int Power { get; set; }
        public int PP { get; set; }
        public int Priority { get; set; }

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
            Numero = JsonHelper.GetIntValue(source, "id");
            MoveId = JsonHelper.GetStringValue(source, "name").Replace("-", string.Empty);

            //CobblePedia.Pedia.FR.Add(KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
            //CobblePedia.Pedia.EN.Add(KeyName, JsonHelper.GetTranslation(source, "en", "name"));

            CobblePedia.Pedia.FR.Add(KeyName, JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, string.Format("cobblemon.move.{0}", MoveId)));
            CobblePedia.Pedia.EN.Add(KeyName, JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, string.Format("cobblemon.move.{0}", MoveId)));
            CobblePedia.Pedia.FR.Add(KeyFlavor, JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, string.Format("cobblemon.move.{0}.desc", MoveId)));
            CobblePedia.Pedia.EN.Add(KeyFlavor, JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, string.Format("cobblemon.move.{0}.desc", MoveId)));
            CobblePedia.Pedia.FR.Add(KeyEffect, JsonHelper.GetEffectEntry(source, "fr"));
            CobblePedia.Pedia.EN.Add(KeyEffect, JsonHelper.GetEffectEntry(source, "en"));

            DamageClass = JsonHelper.GetStringValue(source, "damage_class", "damage_class.name");
            Target = JsonHelper.GetStringValue(source, "target", "target.name");
            MoveType = JsonHelper.GetStringValue(source, "type", "type.name");
            Accuracy = JsonHelper.GetIntValue(source, "accuracy");
            Power = JsonHelper.GetIntValue(source, "power");
            PP = JsonHelper.GetIntValue(source, "pp");
            Priority = JsonHelper.GetIntValue(source, "priority");
        }
    }
}
