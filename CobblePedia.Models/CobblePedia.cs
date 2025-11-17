namespace CobblePedia.Models
{
    using System.Linq;

    using global::CobblePedia.Models.Cobblemon;
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class CobblePedia
    {
        public Dictionary<string, string> FR { get; private set; }
        public Dictionary<string, string> EN { get; private set; }
        public Dictionary<string, Generation> Generations { get; private set; }
        public Dictionary<string, PokemonType> Types { get; private set; }
        public Dictionary<string, Nature> Natures { get; private set; }
        public Dictionary<string, Ability> Abilities { get; private set; }
        public Dictionary<string, Move> Moves { get; private set; }
        public Dictionary<string, EggGroup> EggGroups { get; private set; }
        public Dictionary<string, ExperienceGroup> ExperienceGroups { get; private set; }
        public Dictionary<string, Sprite> Sprites { get; private set; }
        public Dictionary<string, Pokemon> Pokemon { get; private set; }
        public Dictionary<string, Trainer> Trainers { get; private set; }
        public Dictionary<string, Species> PokemonSpecies { get; private set; }

        private static CobblePedia? pedia;

        internal List<Spawn> Spawns { get; private set; }
        internal JObject translationFR;
        internal JObject translationEN;
        internal JObject minecraftFR;
        internal JObject minecraftEN;

        public static CobblePedia Pedia
        {
            get
            {
                if (null == pedia)
                {
                    pedia = new CobblePedia();
                }

                return pedia;
            }
        }

        private CobblePedia()
        {
            this.FR = new Dictionary<string, string>();
            this.EN = new Dictionary<string, string>();
            this.Generations = new Dictionary<string, Generation>();
            this.Types = new Dictionary<string, PokemonType>();
            this.Natures = new Dictionary<string, Nature>();
            this.Abilities = new Dictionary<string, Ability>();
            this.Moves = new Dictionary<string, Move>();
            this.EggGroups = new Dictionary<string, EggGroup>();
            this.ExperienceGroups = new Dictionary<string, ExperienceGroup>();
            this.Sprites = new Dictionary<string, Sprite>();
            this.Pokemon = new Dictionary<string, Pokemon>();
            this.Spawns = new List<Spawn>();
            this.Trainers = new Dictionary<string, Trainer>();

            Load();
        }

        /// <summary>
        /// Chargement des données lorsque la base est déjà alimentée (dataset)
        /// </summary>
        public bool Load()
        {
            string filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "fr.json");
            bool loaded = File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.FR = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "en.json");
            loaded = File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.EN = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Generation.FileName);
            loaded = File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Generations = JsonConvert.DeserializeObject<Dictionary<string, Generation>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, PokemonType.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Types = JsonConvert.DeserializeObject<Dictionary<string, PokemonType>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Nature.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Natures = JsonConvert.DeserializeObject<Dictionary<string, Nature>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Ability.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Abilities = JsonConvert.DeserializeObject<Dictionary<string, Ability>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Move.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Moves = JsonConvert.DeserializeObject<Dictionary<string, Move>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, EggGroup.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.EggGroups = JsonConvert.DeserializeObject<Dictionary<string, EggGroup>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, ExperienceGroup.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.ExperienceGroups = JsonConvert.DeserializeObject<Dictionary<string, ExperienceGroup>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Sprite.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Sprites = JsonConvert.DeserializeObject<Dictionary<string, Sprite>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Spawn.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Spawns = JsonConvert.DeserializeObject<List<Spawn>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Models.Pokemon.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Pokemon = JsonConvert.DeserializeObject<Dictionary<string, Pokemon>>(jsonContent);
            }

            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Trainer.FileName);
            loaded = loaded && File.Exists(filePath);
            if (loaded)
            {
                string jsonContent = File.ReadAllText(filePath);
                this.Trainers = JsonConvert.DeserializeObject<Dictionary<string, Trainer>>(jsonContent);
            }

            return loaded;
        }

        public void BuildData(string basePath, string pathToCobblemon, string pathToRtcmod)
        {
            this.PokemonSpecies = new Dictionary<string, Species>();

            FR.Clear();
            EN.Clear();

            string filePath = Path.Combine(basePath, "fr_fr.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                minecraftFR = JObject.Parse(jsonContent);
            }

            filePath = Path.Combine(basePath, "en_us.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                minecraftEN = JObject.Parse(jsonContent);
            }

            string extractPath = LoadCobblemonPart1(pathToCobblemon);
            LoadPokeAPI();
            LoadCobblemonPart2(extractPath);
            LoadRtcMod(pathToRtcmod);

            CompleteMissingL10N();

            CheckEvolutions();
            CheckTrainersTeam();

            Save();
        }

        private void CheckEvolutions()
        {
            Console.WriteLine("Pokemon PreEvolution - Invalid links");
            foreach (Pokemon item in Pokemon.Values)
            {
                if (item.PreEvolutionSpeciesId == null)
                {
                    continue;
                }

                if (!Pokemon.ContainsKey(item.PreEvolutionSpeciesId))
                {
                    Console.WriteLine("  -> \"{0}\" / \"EvolveTo\": \"{1}", item.SpeciesId, item.PreEvolutionSpeciesId);
                }
            }
            Console.WriteLine("Pokemon Evolution - Invalid links");
            foreach (Pokemon item in Pokemon.Values)
            {
                foreach (Evolution evolution in item.Evolutions)
                {
                    if (!Pokemon.ContainsKey(evolution.EvolveTo))
                    {
                        Console.WriteLine("  -> \"{0}\" / \"EvolveTo\": \"{1}", item.SpeciesId, evolution.EvolveTo);
                    }
                }
            }
        }

        private void CheckTrainersTeam()
        {
            Console.WriteLine("Trainer Team - Invalid links");
            foreach (Trainer trainer in Trainers.Values)
            {
                foreach (TrainerTeam team in trainer.Teams)
                {
                    if (!Pokemon.ContainsKey(team.SpeciesId))
                    {
                        Console.WriteLine("  -> {0} / {1}", trainer.TrainerId, team.SpeciesId);
                    }
                }
            }
        }

        private void CompleteMissingL10N()
        {
            foreach (Pokemon pokemon in Pokemon.Values)
            {
                foreach (Drop drop in pokemon.Drops)
                {
                    string key = string.Format("item.{0}", drop.DropId.Replace(':', '.'));

                    if (drop.DropId.StartsWith("cobblemon:"))
                    {
                        if (!FR.ContainsKey(drop.DropId))
                        {
                            FR.Add(drop.DropId, JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, key));
                            EN.Add(drop.DropId, JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, key));
                        }
                    }
                    if (drop.DropId.StartsWith("minecraft:"))
                    {
                        if (!FR.ContainsKey(drop.DropId))
                        {
                            if (CobblePedia.Pedia.minecraftEN.ContainsKey(key))
                            {
                                FR.Add(drop.DropId, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftFR, key));
                                EN.Add(drop.DropId, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftEN, key));
                            }
                            else
                            {
                                key = string.Format("block.{0}", drop.DropId.Replace(':', '.'));
                                if (CobblePedia.Pedia.minecraftEN.ContainsKey(key))
                                {
                                    FR.Add(drop.DropId, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftFR, key));
                                    EN.Add(drop.DropId, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftEN, key));
                                }
                                else
                                {
                                    FR.Add(drop.DropId, drop.DropId);
                                    EN.Add(drop.DropId, drop.DropId);
                                }
                            }
                        }
                    }
                }
            }
            foreach (Trainer trainer in Trainers.Values)
            {
                if (null == trainer.SignatureItem)
                    continue;

                string key = string.Format("item.{0}", trainer.SignatureItem.Replace(':', '.'));

                if (trainer.SignatureItem.StartsWith("cobblemon:"))
                {
                    if (!FR.ContainsKey(trainer.SignatureItem))
                    {
                        FR.Add(trainer.SignatureItem, JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, key));
                        EN.Add(trainer.SignatureItem, JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, key));
                    }
                }
                if (trainer.SignatureItem.StartsWith("minecraft:"))
                {
                    if (!FR.ContainsKey(trainer.SignatureItem))
                    {
                        if (CobblePedia.Pedia.minecraftEN.ContainsKey(key))
                        {
                            FR.Add(trainer.SignatureItem, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftFR, key));
                            EN.Add(trainer.SignatureItem, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftEN, key));
                        }
                        else
                        {
                            key = string.Format("block.{0}", trainer.SignatureItem.Replace(':', '.'));
                            if (CobblePedia.Pedia.minecraftEN.ContainsKey(key))
                            {
                                FR.Add(trainer.SignatureItem, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftFR, key));
                                EN.Add(trainer.SignatureItem, JsonHelper.GetTranslation(CobblePedia.Pedia.minecraftEN, key));
                            }
                            else
                            {
                                FR.Add(trainer.SignatureItem, trainer.SignatureItem);
                                EN.Add(trainer.SignatureItem, trainer.SignatureItem);
                            }
                        }
                    }
                }
            }
        }

        private void LoadCustomL10N()
        {
            FR.Add("SpriteDefault", "Normal");
            FR.Add("SpriteMale", "Mâle");
            FR.Add("SpriteFemale", "Femelle");
            FR.Add("SpriteDefaultShiny", "Chromatique");
            FR.Add("SpriteMaleShiny", "Mâle Chromatique");
            FR.Add("SpriteFemaletShiny", "Femelle Chromatique");

            EN.Add("SpriteDefault", "Normal");
            EN.Add("SpriteMale", "Male");
            EN.Add("SpriteFemale", "Female");
            EN.Add("SpriteDefaultShiny", "Shiny");
            EN.Add("SpriteMaleShiny", "Male Shiny");
            EN.Add("SpriteFemaletShiny", "Female Shiny");

            FR.Add("Evolution_Method_level_up", "Par niveau (>={0})");
            FR.Add("Evolution_Method_friendship", "Par amitié (>={0})");
            FR.Add("Evolution_Method_battle_critical_hits", "Par attaques critiques (>={0})");
            FR.Add("Evolution_Method_item_interact", "Par objet ({0})");
            FR.Add("Evolution_Method_trade", "Par échange ({0})");

            EN.Add("Evolution_Method_level_up", "By level (>={0})");
            EN.Add("Evolution_Method_friendship", "By friendship (>={0})");
            EN.Add("Evolution_Method_battle_critical_hits", "By critical hits (>={0})");
            EN.Add("Evolution_Method_item_interact", "By item ({0})");
            EN.Add("Evolution_Method_trade", "By trade ({0})");

            FR.Add("cobblemon:thunder_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.thunder_stone"));
            FR.Add("cobblemon:moon_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.moon_stone"));
            FR.Add("cobblemon:fire_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.fire_stone"));
            FR.Add("cobblemon:leaf_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.leaf_stone"));
            FR.Add("cobblemon:sun_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.sun_stone"));
            FR.Add("cobblemon:water_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.water_stone"));
            FR.Add("cobblemon:ice_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.ice_stone"));
            FR.Add("cobblemon:shiny_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.shiny_stone"));
            FR.Add("cobblemon:dusk_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.dusk_stone"));
            FR.Add("cobblemon:dawn_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.dawn_stone"));
            FR.Add("cobblemon:black_augurite", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.black_augurite"));
            FR.Add("cobblemon:peat_block", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.peat_block"));
            FR.Add("cobblemon:shell_helmet", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.shell_helmet"));
            FR.Add("cobblemon:link_cable", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.link_cable"));
            FR.Add("cobblemon:tart_apple", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.tart_apple"));
            FR.Add("cobblemon:sweet_apple", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.sweet_apple"));
            FR.Add("cobblemon:syrupy_apple", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.syrupy_apple"));
            FR.Add("cobblemon:cracked_pot", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.cracked_pot"));
            FR.Add("cobblemon:metal_coat", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.metal_coat"));
            FR.Add("cobblemon:auspicious_armor", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.auspicious_armor"));
            FR.Add("cobblemon:malicious_armor", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.malicious_armor"));
            FR.Add("cobblemon:unremarkable_teacup", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.unremarkable_teacup"));
            FR.Add("cobblemon:galarica_cuff", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.galarica_cuff"));
            FR.Add("cobblemon:galarica_wreath", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.galarica_wreath"));
            FR.Add("cobblemon:chipped_pot", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.chipped_pot"));
            FR.Add("cobblemon:masterpiece_teacup", JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, "item.cobblemon.masterpiece_teacup"));

            EN.Add("cobblemon:thunder_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.thunder_stone"));
            EN.Add("cobblemon:moon_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.moon_stone"));
            EN.Add("cobblemon:fire_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.fire_stone"));
            EN.Add("cobblemon:leaf_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.leaf_stone"));
            EN.Add("cobblemon:sun_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.sun_stone"));
            EN.Add("cobblemon:water_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.water_stone"));
            EN.Add("cobblemon:ice_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.ice_stone"));
            EN.Add("cobblemon:shiny_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.shiny_stone"));
            EN.Add("cobblemon:dusk_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.dusk_stone"));
            EN.Add("cobblemon:dawn_stone", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.dawn_stone"));
            EN.Add("cobblemon:black_augurite", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.black_augurite"));
            EN.Add("cobblemon:peat_block", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.peat_block"));
            EN.Add("cobblemon:shell_helmet", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.shell_helmet"));
            EN.Add("cobblemon:link_cable", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.link_cable"));
            EN.Add("cobblemon:tart_apple", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.tart_apple"));
            EN.Add("cobblemon:sweet_apple", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.sweet_apple"));
            EN.Add("cobblemon:syrupy_apple", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.syrupy_apple"));
            EN.Add("cobblemon:cracked_pot", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.cracked_pot"));
            EN.Add("cobblemon:metal_coat", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.metal_coat"));
            EN.Add("cobblemon:auspicious_armor", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.auspicious_armor"));
            EN.Add("cobblemon:malicious_armor", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.malicious_armor"));
            EN.Add("cobblemon:unremarkable_teacup", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.unremarkable_teacup"));
            EN.Add("cobblemon:galarica_cuff", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.galarica_cuff"));
            EN.Add("cobblemon:galarica_wreath", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.galarica_wreath"));
            EN.Add("cobblemon:chipped_pot", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.chipped_pot"));
            EN.Add("cobblemon:masterpiece_teacup", JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, "item.cobblemon.masterpiece_teacup"));

            FR.Add("Trainer_Type_", "");
            FR.Add("Trainer_Type_leader", "Leader");
            FR.Add("Trainer_Type_battleground", "Champ de bataille");
            FR.Add("Trainer_Type_team_rocket", "Team Rocket");
            FR.Add("Trainer_Type_team_shadow", "Team Shadow");
            FR.Add("Trainer_Type_champ", "Champion");
            FR.Add("Trainer_Type_normal", "Normal");
            FR.Add("Trainer_Type_rival", "Rival");
            FR.Add("Trainer_Type_team_galactic", "Team Galactic");
            FR.Add("Trainer_Type_e4", "Conseil des 4");
            FR.Add("Trainer_Type_ligh_of_ruin", "Ligh of ruin");

            EN.Add("Trainer_Type_", "");
            EN.Add("Trainer_Type_leader", "Leader");
            EN.Add("Trainer_Type_battleground", "Battleground");
            EN.Add("Trainer_Type_team_rocket", "Team Rocket");
            EN.Add("Trainer_Type_team_shadow", "Team Shadow");
            EN.Add("Trainer_Type_champ", "Champion");
            EN.Add("Trainer_Type_normal", "Normal");
            EN.Add("Trainer_Type_rival", "Rival");
            EN.Add("Trainer_Type_team_galactic", "Team Galactic");
            EN.Add("Trainer_Type_e4", "Conseil des 4");
            EN.Add("Trainer_Type_ligh_of_ruin", "Ligh of ruin");

            FR.Add("Trainer_Series_", "");
            FR.Add("Trainer_Series_unbound", "Illimité");
            FR.Add("Trainer_Series_radicalred", "Radical Rouge");
            FR.Add("Trainer_Series_bdsp", "BDSP");

            EN.Add("Trainer_Series_", "");
            EN.Add("Trainer_Series_unbound", "Unbound");
            EN.Add("Trainer_Series_radicalred", "Radical Red");
            EN.Add("Trainer_Series_bdsp", "BDSP");

            FR.Add("Moves_Egg", "Eclosion");
            FR.Add("Moves_Level", "Niveau {0}");
            FR.Add("Moves_TM", "CT/TM");
            FR.Add("Moves_Tutor", "Tuteur");

            EN.Add("Moves_Egg", "Egg");
            EN.Add("Moves_Level", "Level {0}");
            EN.Add("Moves_TM", "CT/CM");
            EN.Add("Moves_Tutor", "Tutor");
        }

        private void LoadPokeAPI()
        {
            LoadGenerations(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Generation.BasePath));
            LoadTypes(Path.Combine(ConfigurationHelper.CobblePediaDataPath, PokemonType.BasePath));
            LoadNatures(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Nature.BasePath));
            LoadAbilities(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Ability.BasePath));
            LoadMoves(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Move.BasePath));
            LoadEggGroups(Path.Combine(ConfigurationHelper.CobblePediaDataPath, EggGroup.BasePath));
            LoadExperienceGroups(Path.Combine(ConfigurationHelper.CobblePediaDataPath, ExperienceGroup.BasePath));
            LoadSprites(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Sprite.BasePath));
        }

        public string LoadCobblemonPart1(string file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string extractPath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, fileName);
            JarHelper.ExtractSubfolder(file, extractPath, RctmodJar.Paths, true);

            LoadLanguages(extractPath);
            LoadCustomL10N();

            return extractPath;
        }
        public void LoadCobblemonPart2(string extractPath)
        {
            LoadSpawns(extractPath);
            LoadPokemon(extractPath);
        }

        public void LoadRtcMod(string file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string extractPath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, fileName);
            JarHelper.ExtractSubfolder(file, extractPath, RctmodJar.Paths, true);

            LoadTrainers(extractPath);
        }

        private void Save()
        {
            string jsonContent = JsonConvert.SerializeObject(this.FR, Formatting.Indented);
            string filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "fr.json");
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.EN, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "en.json");
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Generations, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Generation.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Types, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, PokemonType.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Natures, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Nature.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Abilities, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Ability.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Moves, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Move.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Spawns, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Spawn.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.EggGroups, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, EggGroup.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.ExperienceGroups, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, ExperienceGroup.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Sprites, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Sprite.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Pokemon, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Models.Pokemon.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Trainers, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Trainer.FileName);
            File.WriteAllText(filePath, jsonContent);
        }

        #region Cobblemon
        private void LoadLanguages(string basePath)
        {
            Console.WriteLine("LoadLanguages");

            string filePath = Path.Combine(Path.Combine(basePath, CobblemonJar.Paths[0]), "fr_fr.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                translationFR = JObject.Parse(jsonContent);
            }

            filePath = Path.Combine(Path.Combine(basePath, CobblemonJar.Paths[0]), "en_us.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                translationEN = JObject.Parse(jsonContent);
            }
        }

        private void LoadSpawns(string basePath)
        {
            Console.WriteLine("LoadSpawns");

            Spawns.Clear();

            string searchPath = Path.Combine(basePath, CobblemonJar.Paths[2]);
            foreach (string file in Directory.EnumerateFiles(searchPath))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                string fileName = Path.GetFileNameWithoutExtension(file);
                int speciesId = int.Parse(fileName.Substring(0, 4));
                foreach (JToken token in source.SelectTokens("spawns"))
                {
                    foreach (JToken item in token.Children())
                    {
                        Spawn condition = new Spawn(item, speciesId);
                        Spawns.Add(condition);
                    }
                }
            }
        }

        private void LoadPokemon(string basePath)
        {
            Console.WriteLine("LoadPokemon");

            Pokemon.Clear();

            List<Species> items = new List<Species>();
            string searchPath = Path.Combine(basePath, CobblemonJar.Paths[1]);
            foreach (string path in Directory.EnumerateDirectories(searchPath))
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    string jsonContent = File.ReadAllText(file);
                    JObject source = JObject.Parse(jsonContent);
                    Species species = new Species(source);
                    items.Add(species);
                }
            }

            List<Pokemon> ordered = new List<Pokemon>();
            foreach (Species item in items)
            {
                Pokemon pokemon = new Pokemon(item);
                ordered.Add(pokemon);

                int i = 0;
                foreach (Species form in item.Forms)
                {
                    pokemon = new Pokemon(item, form, ++i);
                    ordered.Add(pokemon);
                }
            }

            foreach (Pokemon item in ordered.OrderBy(item => item.NationalPokedexNumber).ToList<Pokemon>())
            {
                Pokemon.Add(item.SpeciesId, item);
            }
        }
        #endregion

        //#region Cobblemon Challenge
        private void LoadTrainers(string basePath)
        {
            Console.WriteLine("LoadTrainers");

            Trainers.Clear();

            string searchPath = Path.Combine(basePath, RctmodJar.Paths[6]);
            foreach (string file in Directory.EnumerateFiles(searchPath))
            {
                string jsonContent = File.ReadAllText(file);
                string filename = Path.GetFileNameWithoutExtension(file);
                JObject source = JObject.Parse(jsonContent);
                Trainer trainer = new Trainer(source, filename, basePath);
                Trainers.Add(trainer.TrainerId, trainer);
            }
        }

        //#endregion

        #region PokeAPI
        private void LoadGenerations(string basePath)
        {
            Console.WriteLine("LoadGenerations");

            Generations.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Generation generation = new Generation(source);
                Generations.Add(generation.GenerationId, generation);
            }
        }

        private void LoadTypes(string basePath)
        {
            Console.WriteLine("LoadTypes");

            Types.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                PokemonType pokemonType = new PokemonType(source);
                Types.Add(pokemonType.TypeId, pokemonType);
            }
        }

        private void LoadNatures(string basePath)
        {
            Console.WriteLine("LoadNatures");

            Natures.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Nature nature = new Nature(source);
                Natures.Add(nature.NatureId, nature);
            }
        }

        private void LoadAbilities(string basePath)
        {
            Console.WriteLine("LoadAbilities");

            Abilities.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Ability ability = new Ability(source);
                Abilities.Add(ability.AbilityId, ability);
            }
        }

        private void LoadMoves(string basePath)
        {
            Console.WriteLine("LoadMoves");

            Moves.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Move move = new Move(source);
                Moves.Add(move.MoveId, move);
            }
        }

        private void LoadEggGroups(string basePath)
        {
            Console.WriteLine("LoadEggGroups");

            EggGroups.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                EggGroup eggGroup = new EggGroup(source);
                EggGroups.Add(eggGroup.EggGroupId, eggGroup);
            }
        }

        private void LoadExperienceGroups(string basePath)
        {
            Console.WriteLine("LoadExperienceGroups");

            ExperienceGroups.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                ExperienceGroup experienceGroup = new ExperienceGroup(source);
                ExperienceGroups.Add(experienceGroup.ExperienceGroupId, experienceGroup);
            }
        }

        private void LoadSprites(string basePath)
        {
            Console.WriteLine("LoadSprites");

            Sprites.Clear();
            Sprites.Add("none", new Sprite());

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Sprite sprite = new Sprite(source);
                Sprites.Add(sprite.Name, sprite);
            }
        }
        #endregion
    }
}
