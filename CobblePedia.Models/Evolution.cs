namespace CobblePedia.Models
{
    using System.Collections.Generic;

    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Evolution
    {
        public string Method { get; set; }
        public string EvolveTo { get; set; }
        public bool IsByLevel { get; set; }
        public int MinLevel { get; set; }
        public bool IsByFriendship { get; set; }
        public int MinFriendship { get; set; }
        public bool IsByBattleCriticalHits { get; set; }
        public int MinBattleCriticalHits { get; set; }
        public bool IsByItem { get; set; }
        public string ItemKey { get; set; }
        public bool IsByTrade { get; set; }
        public bool IsConsumeHeldItem { get; set; }
        public List<string> LearnableMoves { get; set; }

        public Evolution(JToken token)
        {
            if (token == null)
                return;

            Method = token.Value<string>("variant");
            EvolveTo = NamingHelper.RebuildPokemonName(token.Value<string>("result"));
            switch (Method)
            {
                case "level_up":
                    switch (token.SelectToken("requirements[0].variant").Value<string>())
                    {
                        case "friendship":
                            Method = "friendship";
                            IsByFriendship = true;
                            MinFriendship = token.SelectToken("requirements[0].amount").Value<int>();
                            break;
                        case "battle_critical_hits":
                            Method = "battle_critical_hits";
                            IsByBattleCriticalHits = true;
                            MinBattleCriticalHits = token.SelectToken("requirements[0].amount").Value<int>();
                            break;
                        case "level":
                            IsByLevel = true;
                            MinLevel = token.SelectToken("requirements[0].minLevel").Value<int>();
                            break;
                        case "properties":
                            Method = "properties";
                            break;
                        case "property_range":
                            Method = "property_range";
                            break;
                    }
                    break;
                case "item_interact":
                    IsByItem = true;
                    ItemKey = token.Value<string>("requiredContext");
                    break;
                case "trade":
                    IsByTrade = true;
                    /*TODO*/
                    break;
            }
            IsConsumeHeldItem = token.Value<bool>("consumeHeldItem");
            LearnableMoves = new List<string>(token.SelectTokens("learnableMoves").Values<string>());
        }
    }
}