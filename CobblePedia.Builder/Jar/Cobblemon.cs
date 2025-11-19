namespace CobblePedia.Builder.Jar
{
    using CobblePedia.Models;
    using CobblePedia.Models.Utils;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal sealed class Cobblemon
    {
        internal static readonly string[] Paths =
            {
                "assets/cobblemon/lang",
                "data/cobblemon/species",
                "data/cobblemon/spawn_pool_world",
                "data/cobblemon/spawning",
                "data/cobblemon/dexes",
                "data/cobblemon/berries",
                "assets/cobblemon/textures/item"
            };

        private string root;
        internal List<Spawn> Spawns { get; private set; }

        public Dictionary<string, Pokemon> Pokemon { get; private set; }
        public JObject FR { get; private set; }
        public JObject EN { get; private set; }

        public Cobblemon(string jarPath)
        {
            root = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "Cobblemon");
            JarHelper.ExtractSubfolder(jarPath, root, Cobblemon.Paths, true);

            Pokemon = new Dictionary<string, Pokemon>();
            Spawns = new List<Spawn>();

            LoadLanguages();
            LoadSpawns(root);
            LoadPokemon(root);

            CheckEvolutions();
        }

        private void LoadLanguages()
        {
            Console.WriteLine("Cobblemon Load languages");

            string filePath = Path.Combine(Path.Combine(root, Cobblemon.Paths[0]), "fr_fr.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                FR = JObject.Parse(jsonContent);
            }

            filePath = Path.Combine(Path.Combine(root, Cobblemon.Paths[0]), "en_us.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                EN = JObject.Parse(jsonContent);
            }
        }

        private void LoadSpawns(string basePath)
        {
            Console.WriteLine("Cobblemon Load Spawns");

            string searchPath = Path.Combine(basePath, Cobblemon.Paths[2]);
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
            bool IsMissing = true;
            Console.WriteLine("Cobblemon Load Pokemon");

            List<Species> items = new List<Species>();
            string searchPath = Path.Combine(basePath, Cobblemon.Paths[1]);
            foreach (string path in Directory.EnumerateDirectories(searchPath))
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    string jsonContent = File.ReadAllText(file);
                    JObject source = JObject.Parse(jsonContent);
                    Species species = new Species(source);

                    species.Spawns = Spawns.Where(item => item.NationalPokedexNumber == species.NationalPokedexNumber).ToList();
                    if (Builder.Instance.pokeAPI.Sprites.ContainsKey(species.GetSpriteId()))
                    {
                        species.Picture = Builder.Instance.pokeAPI.Sprites[species.GetSpriteId()];
                    }
                    else
                    {
                        if (IsMissing)
                        {
                            Console.WriteLine("Missing sprites");
                            IsMissing = false;
                        }
                        Console.WriteLine("  - {2} ({1:D4} / ${0})", species.SpeciesId, species.NationalPokedexNumber, species.GetSpriteId());
                        species.Picture = Builder.Instance.pokeAPI.Sprites["none"];
                    }


                    if (null != species.GetTranslateKey())
                    {
                        Builder.Instance.FR.Add(species.KeyDescription, JsonHelper.GetTranslation(FR, species.GetTranslateKey()));
                        Builder.Instance.FR.Add(species.KeyName, JsonHelper.GetTranslation(FR, species.GetTranslateKey().Replace(".desc", ".name")));
                        Builder.Instance.EN.Add(species.KeyDescription, JsonHelper.GetTranslation(EN, species.GetTranslateKey()));
                        Builder.Instance.EN.Add(species.KeyName, JsonHelper.GetTranslation(EN, species.GetTranslateKey().Replace(".desc", ".name")));
                    }

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
                    if (Builder.Instance.pokeAPI.Sprites.ContainsKey(form.GetSpriteId()))
                    {
                        form.Picture = Builder.Instance.pokeAPI.Sprites[form.GetSpriteId()];
                    }
                    else
                    {
                        if (IsMissing)
                        {
                            Console.WriteLine("Missing sprites");
                            IsMissing = false;
                        }
                        Console.WriteLine("  - {2} ({1:D4} / ${0})", form.SpeciesId, form.NationalPokedexNumber, form.GetSpriteId());
                        form.Picture = Builder.Instance.pokeAPI.Sprites["none"];
                    }

                    if (null != form.GetTranslateKey())
                    {
                        Builder.Instance.FR.Add(form.KeyDescription, JsonHelper.GetTranslation(FR, form.GetTranslateKey()));
                        Builder.Instance.FR.Add(form.KeyName, JsonHelper.GetTranslation(FR, form.GetTranslateKey().Replace(".desc", ".name")));
                        Builder.Instance.EN.Add(form.KeyDescription, JsonHelper.GetTranslation(EN, form.GetTranslateKey()));
                        Builder.Instance.EN.Add(form.KeyName, JsonHelper.GetTranslation(EN, form.GetTranslateKey().Replace(".desc", ".name")));
                    }

                    pokemon = new Pokemon(item, form, ++i);
                    if (!Builder.Instance.EN.ContainsKey(pokemon.KeyName))
                    {
                        Builder.Instance.FR.Add(pokemon.KeyDescription, "-");
                        Builder.Instance.FR.Add(pokemon.KeyName, "-");
                        Builder.Instance.EN.Add(pokemon.KeyDescription, "-");
                        Builder.Instance.EN.Add(pokemon.KeyName, "-");
                    }

                    ordered.Add(pokemon);
                }
            }

            foreach (Pokemon item in ordered.OrderBy(item => item.NationalPokedexNumber).ToList<Pokemon>())
            {
                Pokemon.Add(item.SpeciesId, item);
            }
        }

        private void CheckEvolutions()
        {
            Console.WriteLine("Cobblemon - Check evolution invalid links");

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

        internal void Save()
        {
            Console.WriteLine("Cobblemon Save");

            string jsonContent = JsonConvert.SerializeObject(this.Spawns, Formatting.Indented);
            string filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Spawn.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Pokemon, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Models.Pokemon.FileName);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}
