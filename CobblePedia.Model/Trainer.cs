namespace CobblePedia.Models
{
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

        public Trainer() { }

        public Trainer(JObject source, string id, string basePath)
        {
            TrainerId = id;

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
        }
    }
}