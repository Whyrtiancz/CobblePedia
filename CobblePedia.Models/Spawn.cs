namespace CobblePedia.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public class Spawn
    {
        public static string FileName { get; } = "spawns.json";

        public int NationalPokedexNumber { get; set; }
        public string SpawnId { get; set; }
        public string Type { get; set; }
        public string Context { get; set; }
        public string Bucket { get; set; }
        public string Level { get; set; }
        public float Weight { get; set; }
        public List<string> Presets { get; set; }
        public int MinSkyLight { get; set; }
        public int MaxSkyLight { get; set; }
        public int MaxY { get; set; }
        public bool CanSeeSky { get; set; }
        public bool IsRaining { get; set; }
        public List<string> Biomes { get; set; }
        public List<string> NeededNearbyBlocks { get; set; }
        public List<string> AntiConditionBiomes { get; set; }

        internal Spawn() { }
        internal Spawn(JToken token, int nationalPokedexNumber)
        {
            SpawnId = token.Value<string>("id");
            NationalPokedexNumber = nationalPokedexNumber;
            Presets = new List<string>(token.SelectTokens("presets").Values<string>());
            Type = token.Value<string>("type");
            Context = token.Value<string>("context");
            Bucket = token.Value<string>("bucket");
            Level = token.Value<string>("level");
            Weight = token.Value<float>("weight");

            /* Gestion des conditions */
            Biomes = new List<string>();
            NeededNearbyBlocks = new List<string>();

            JToken condition = token.SelectToken("condition");
            if (condition != null)
            {
                if (null != condition.SelectToken("canSeeSky"))
                {
                    CanSeeSky = condition.Value<bool>("canSeeSky");
                }
                if (null != condition.SelectToken("biomes"))
                {
                    Biomes = new List<string>(condition.SelectTokens("biomes").Values<string>());
                }
                if (null != condition.SelectToken("minSkyLight"))
                {
                    MinSkyLight = condition.Value<int>("minSkyLight");
                }
                if (null != condition.SelectToken("maxSkyLight"))
                {
                    MaxSkyLight = condition.Value<int>("maxSkyLight");
                }
                if (null != condition.SelectToken("isRaining"))
                {
                    IsRaining = condition.Value<bool>("isRaining");
                }
                if (null != condition.SelectToken("maxY"))
                {
                    MaxY = condition.Value<int>("maxY");
                }
                if (null != condition.SelectToken("neededNearbyBlocks"))
                {
                    NeededNearbyBlocks = new List<string>(condition.SelectTokens("neededNearbyBlocks").Values<string>());
                }
            }

            AntiConditionBiomes = new List<string>();
            JToken anticondition = token.SelectToken("anticondition");
            if (anticondition != null)
            {
                if (null != condition.SelectToken("biomes"))
                {
                    AntiConditionBiomes = new List<string>(anticondition.SelectTokens("biomes").Values<string>());
                }
            }
        }
    }
}