namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json;

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
        internal List<Spawn> Spawns { get; private set; }

        private static CobblePedia? pedia;


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
    }
}
