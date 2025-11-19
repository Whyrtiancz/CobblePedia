namespace CobblePedia.Builder.Jar
{
    using System.Globalization;
    using System.Text;

    using CobblePedia.Models;
    using CobblePedia.Models.Utils;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal sealed class Rctmod
    {
        internal static readonly string[] Paths =
        {
            "assets/rctmod/lang",
            "data/rctmod/advancement",
            "data/rctmod/loot_table",
            "data/rctmod/mobs",
            "data/rctmod/series",
            "data/rctmod/trainer_type",
            "data/rctmod/trainers"
        };

        public JObject EN { get; private set; }

        internal static readonly string BattleInfoPath = Path.Combine(Paths[3], "trainers/single");

        internal Dictionary<string, Trainer> Trainers { get; private set; }
        private string root;

        public Rctmod(string jarPath)
        {
            root = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "Cobblemon");
            JarHelper.ExtractSubfolder(jarPath, root, Rctmod.Paths, true);

            this.Trainers = new Dictionary<string, Trainer>();

            LoadLanguages();
            LoadTrainers(root);
        }

        private void LoadLanguages()
        {
            Console.WriteLine("Rtcmod Load languages");

            string filePath = Path.Combine(Path.Combine(root, Rctmod.Paths[0]), "en_us.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                EN = JObject.Parse(jsonContent);
            }

            foreach (JProperty item in EN.Children())
            {
                string key = item.Name;
                string? value = ((JValue)item.Value).ToString();

                if ((key.StartsWith("series.rctmod") || key.StartsWith("trainer_type.rctmod")) && value != null)
                {
                    Builder.Instance.FR.Add(key, value);
                    Builder.Instance.EN.Add(key, value);
                }
            }
        }

        private void LoadTrainers(string basePath)
        {
            Console.WriteLine("Rtcmod Load Trainers");

            Trainers.Clear();

            string searchPath = Path.Combine(basePath, Rctmod.Paths[6]);
            foreach (string file in Directory.EnumerateFiles(searchPath))
            {
                string jsonContent = File.ReadAllText(file);
                string filename = Path.GetFileNameWithoutExtension(file);
                JObject source = JObject.Parse(jsonContent);
                Trainer trainer = new Trainer(source, filename, basePath);

                string[] names = trainer.TrainerId.Split('_');
                StringBuilder nameBuilder = new StringBuilder();
                for (int i = 0; i < names.Length - 1; i++)
                {
                    if (i > 0)
                    {
                        nameBuilder.Append(' ');
                    }
                    nameBuilder.Append(names[i]);
                }

                Builder.Instance.FR.Add(trainer.KeyName, CultureInfo.GetCultureInfo("fr-FR").TextInfo.ToTitleCase(nameBuilder.ToString()));
                Builder.Instance.EN.Add(trainer.KeyName, CultureInfo.GetCultureInfo("en-US").TextInfo.ToTitleCase(nameBuilder.ToString()));

                ReadBattleInfo(basePath, trainer);

                Trainers.Add(trainer.TrainerId, trainer);
            }
        }

        private void ReadBattleInfo(string basePath, Trainer trainer)
        {
            trainer.BiomeTagBlackList = new List<string>();
            trainer.BiomeTagWhiteList = new List<string>();
            trainer.Series = new List<string>();
            trainer.RequiredDefeats = new List<string>();

            string file = Path.Combine(Path.Combine(basePath, Rctmod.BattleInfoPath), trainer.TrainerId + ".json");
            if (!File.Exists(file)) return;

            string jsonContent = File.ReadAllText(file);
            JObject source = JObject.Parse(jsonContent);

            trainer.BattleCountTicks = source.SelectToken("battleCooldownTicks").Value<int>();
            trainer.MaxTrainerDefeats = source.SelectToken("maxTrainerDefeats").Value<int>();
            trainer.MaxTrainerWins = source.SelectToken("maxTrainerWins").Value<int>();
            trainer.IsOptional = source.SelectToken("optional").Value<bool>();
            if (null != source.SelectToken("signatureItem"))
            {
                trainer.SignatureItem = source.SelectToken("signatureItem").Value<string>();
            }
            trainer.SpawnWeightFactor = source.SelectToken("spawnWeightFactor").Value<float>();
            trainer.TrainerType = source.SelectToken("type").Value<string>();

            JToken token = source.SelectToken("biomeTagBlacklist");
            if (token != null)
            {
                trainer.BiomeTagBlackList = new List<string>(token.Values<string>());
            }
            token = source.SelectToken("biomeTagWhitelist");
            if (token != null)
            {
                trainer.BiomeTagWhiteList = new List<string>(token.Values<string>());
            }
            token = source.SelectToken("series");
            if (token != null)
            {
                trainer.Series = new List<string>(token.Values<string>());
            }
            token = source.SelectToken("requiredDefeats");
            if (token != null)
            {
                trainer.RequiredDefeats = new List<string>(token.Children().Values<string>());
            }
        }

        internal void Save()
        {
            Console.WriteLine("Rtcmod Save");

            string jsonContent = JsonConvert.SerializeObject(this.Trainers, Formatting.Indented);
            string filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Trainer.FileName);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}
