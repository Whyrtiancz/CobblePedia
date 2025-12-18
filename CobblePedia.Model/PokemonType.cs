namespace CobblePedia.Models
{
    using System.Collections.Generic;

    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class PokemonType
    {
        public static string FileName { get; } = "types.json";
        public static string BasePath { get; } = @"PokeApi\Types";

        public List<string> DoubleDamageFrom { get; set; }
        public List<string> DoubleDamageTo { get; set; }
        public List<string> HalfDamageFrom { get; set; }
        public List<string> HalfDamageTo { get; set; }
        public List<string> NoDamageFrom { get; set; }
        public List<string> NoDamageTo { get; set; }

        public string TypeId { get; set; }

        public string KeyName
        {
            get
            {
                return string.Format("Type_{0}", TypeId);
            }
        }

        public PokemonType() { }

        public PokemonType(JObject source)
        {
            TypeId = JsonHelper.GetStringValue(source, "name");

            DoubleDamageFrom = new List<string>();
            foreach (JToken token in source.SelectTokens("$.damage_relations.double_damage_from"))
            {
                foreach (string item in token.Children().Values("name"))
                {
                    DoubleDamageFrom.Add(item);
                }
            }

            DoubleDamageTo = new List<string>();
            foreach (JToken token in source.SelectTokens("$.damage_relations.double_damage_to"))
            {
                foreach (string item in token.Children().Values("name"))
                {
                    DoubleDamageTo.Add(item);
                }
            }

            HalfDamageFrom = new List<string>();
            foreach (JToken token in source.SelectTokens("$.damage_relations.half_damage_from"))
            {
                foreach (string item in token.Children().Values("name"))
                {
                    HalfDamageFrom.Add(item);
                }
            }

            HalfDamageTo = new List<string>();
            foreach (JToken token in source.SelectTokens("$.damage_relations.half_damage_to"))
            {
                foreach (string item in token.Children().Values("name"))
                {
                    HalfDamageTo.Add(item);
                }
            }

            NoDamageFrom = new List<string>();
            foreach (JToken token in source.SelectTokens("$.damage_relations.no_damage_from"))
            {
                foreach (string item in token.Children().Values("name"))
                {
                    NoDamageFrom.Add(item);
                }
            }

            NoDamageTo = new List<string>();
            foreach (JToken token in source.SelectTokens("$.damage_relations.no_damage_to"))
            {
                foreach (string item in token.Children().Values("name"))
                {
                    NoDamageTo.Add(item);
                }
            }
        }
    }
}
