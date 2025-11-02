namespace CobblePedia.Models
{
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

        private static CobblePedia? pedia;

        internal List<Spawn> Spawns { get; private set; }
        internal JObject translationFR;
        internal JObject translationEN;

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

        public void BuildData(string pathToCobblemon, string pathToRtcmod)
        {
            FR.Clear();
            EN.Clear();

            LoadCustomL10N();
            LoadPokeAPI();
            LoadCobblemon(pathToCobblemon);
            LoadRtcMod(pathToRtcmod);

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
                    Console.WriteLine("  -> {0} / {1}", item.SpeciesId, item.PreEvolutionSpeciesId);
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

        public void LoadCobblemon(string file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string extractPath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, fileName);
            JarHelper.ExtractSubfolder(file, extractPath, RctmodJar.Paths, true);

            LoadLanguages(extractPath);
            LoadSpawns(extractPath);
            LoadSpecies(extractPath);
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

        private void LoadSpecies(string basePath)
        {
            Console.WriteLine("LoadSpecies");

            Pokemon.Clear();

            List<Pokemon> items = new List<Pokemon>();
            string searchPath = Path.Combine(basePath, CobblemonJar.Paths[1]);
            foreach (string path in Directory.EnumerateDirectories(searchPath))
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    string jsonContent = File.ReadAllText(file);
                    JObject source = JObject.Parse(jsonContent);
                    Pokemon species = new Pokemon(source);
                    items.Add(species);
                }
            }

            foreach (Pokemon item in items.OrderBy(item => item.NationalPokedexNumber).ToList<Pokemon>())
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
