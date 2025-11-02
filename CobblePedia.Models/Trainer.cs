namespace CobblePedia.Models
{
    using System.Globalization;
    using System.Text;

    using global::CobblePedia.Models.Cobblemon;

    using Newtonsoft.Json.Linq;

    public class Trainer
    {
        public static string FileName { get; } = "trainers.json";

        public string TrainerId { get; set; }

        public int BattleCountTicks { get; set; }
        public int MaxTrainerDefeats { get; set; }
        public int MaxTrainerWins { get; set; }
        public bool IsOptional { get; set; }
        public string SignatureItem { get; set; }
        public float SpawnWeightFactor { get; set; }
        public string TrainerType { get; set; }

        public int MaxItemUses { get; set; }
        public List<Bag> Bags { get; set; }
        public List<TrainerTeam> Teams { get; set; }

        public List<string> BiomeTagBlackList { get; set; }
        public List<string> BiomeTagWhiteList { get; set; }
        public List<string> Series { get; set; }
        public List<string> RequiredDefeats { get; set; }


        public string KeyName
        {
            get
            {
                return string.Format(Properties.Resources.TrainerKeyName, TrainerId);
            }
        }

        internal Trainer() { }

        internal Trainer(JObject source, string id, string basePath)
        {
            TrainerId = id;

            string[] names = id.Split('_');
            StringBuilder nameBuilder = new StringBuilder();
            for (int i = 0; i < names.Length - 1; i++)
            {
                if (i > 0)
                {
                    nameBuilder.Append(' ');
                }
                nameBuilder.Append(names[i]);
            }

            CobblePedia.Pedia.FR.Add(KeyName, CultureInfo.GetCultureInfo("fr-FR").TextInfo.ToTitleCase(nameBuilder.ToString()));
            CobblePedia.Pedia.EN.Add(KeyName, CultureInfo.GetCultureInfo("en-US").TextInfo.ToTitleCase(nameBuilder.ToString()));

            JToken token = source.SelectToken("battleRules");
            if (token != null)
            {
                MaxItemUses = token.SelectTokens("maxItemUses").Values<int>().First();
            }
            Bags = new List<Bag>();
            token = source.SelectToken("bag");
            if (token != null)
            {
                foreach (JObject item in token.Children())
                {
                    Bag bag = new Bag(item);
                    Bags.Add(bag);
                }
            }
            Teams = new List<TrainerTeam>();
            token = source.SelectToken("team");
            if (token != null)
            {
                foreach (JObject item in token.Children())
                {
                    TrainerTeam team = new TrainerTeam(item);
                    Teams.Add(team);
                }
            }

            ReadBattleInfo(basePath);
        }

        private void ReadBattleInfo(string basePath)
        {
            BiomeTagBlackList = new List<string>();
            BiomeTagWhiteList = new List<string>();
            Series = new List<string>();
            RequiredDefeats = new List<string>();

            string file = Path.Combine(Path.Combine(basePath, RctmodJar.BattleInfoPath), TrainerId + ".json");
            if (!File.Exists(file)) return;

            string jsonContent = File.ReadAllText(file);
            JObject source = JObject.Parse(jsonContent);

            BattleCountTicks = source.SelectToken("battleCooldownTicks").Value<int>();
            MaxTrainerDefeats = source.SelectToken("maxTrainerDefeats").Value<int>();
            MaxTrainerWins = source.SelectToken("maxTrainerWins").Value<int>();
            IsOptional = source.SelectToken("optional").Value<bool>();
            if (null != source.SelectToken("signatureItem"))
            {
                SignatureItem = source.SelectToken("signatureItem").Value<string>();
            }
            SpawnWeightFactor = source.SelectToken("spawnWeightFactor").Value<float>();
            TrainerType = source.SelectToken("type").Value<string>();

            JToken token = source.SelectToken("biomeTagBlacklist");
            if (token != null)
            {
                BiomeTagBlackList = new List<string>(token.Values<string>());
            }
            token = source.SelectToken("biomeTagWhitelist");
            if (token != null)
            {
                BiomeTagWhiteList = new List<string>(token.Values<string>());
            }
            token = source.SelectToken("series");
            if (token != null)
            {
                Series = new List<string>(token.Values<string>());
            }
            token = source.SelectToken("requiredDefeats");
            if (token != null)
            {
                RequiredDefeats = new List<string>(token.Children().Values<string>());
            }
        }
    }
}