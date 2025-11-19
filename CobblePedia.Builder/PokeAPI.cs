namespace CobblePedia.Builder
{
    using System;
    using System.Collections.Generic;

    using CobblePedia.Models;
    using CobblePedia.Models.Utils;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class PokeAPI
    {
        public Dictionary<string, Generation> Generations { get; private set; }
        public Dictionary<string, PokemonType> Types { get; private set; }
        public Dictionary<string, Nature> Natures { get; private set; }
        public Dictionary<string, Ability> Abilities { get; private set; }
        public Dictionary<string, Move> Moves { get; private set; }
        public Dictionary<string, EggGroup> EggGroups { get; private set; }
        public Dictionary<string, ExperienceGroup> ExperienceGroups { get; private set; }
        public Dictionary<string, Sprite> Sprites { get; private set; }

        public PokeAPI()
        {
            this.Generations = new Dictionary<string, Generation>();
            this.Types = new Dictionary<string, PokemonType>();
            this.Natures = new Dictionary<string, Nature>();
            this.Abilities = new Dictionary<string, Ability>();
            this.Moves = new Dictionary<string, Move>();
            this.EggGroups = new Dictionary<string, EggGroup>();
            this.ExperienceGroups = new Dictionary<string, ExperienceGroup>();
            this.Sprites = new Dictionary<string, Sprite>();

            LoadGenerations(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Generation.BasePath));
            LoadTypes(Path.Combine(ConfigurationHelper.CobblePediaDataPath, PokemonType.BasePath));
            LoadNatures(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Nature.BasePath));
            LoadAbilities(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Ability.BasePath));
            LoadEggGroups(Path.Combine(ConfigurationHelper.CobblePediaDataPath, EggGroup.BasePath));
            LoadExperienceGroups(Path.Combine(ConfigurationHelper.CobblePediaDataPath, ExperienceGroup.BasePath));
            LoadSprites(Path.Combine(ConfigurationHelper.CobblePediaDataPath, Sprite.BasePath));
        }

        private void LoadGenerations(string basePath)
        {
            Console.WriteLine("PokeAPI Load Generations");

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Generation generation = new Generation(source);

                Builder.Instance.FR.Add(generation.KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
                Builder.Instance.EN.Add(generation.KeyName, JsonHelper.GetTranslation(source, "en", "name"));

                Generations.Add(generation.GenerationId, generation);
            }
        }

        private void LoadTypes(string basePath)
        {
            Console.WriteLine("PokeAPI Load Types");

            Types.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                PokemonType pokemonType = new PokemonType(source);

                Builder.Instance.FR.Add(pokemonType.KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
                Builder.Instance.EN.Add(pokemonType.KeyName, JsonHelper.GetTranslation(source, "en", "name"));

                Types.Add(pokemonType.TypeId, pokemonType);
            }
        }

        private void LoadNatures(string basePath)
        {
            Console.WriteLine("PokeAPI Load Natures");

            Natures.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Nature nature = new Nature(source);

                Builder.Instance.FR.Add(nature.KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
                Builder.Instance.EN.Add(nature.KeyName, JsonHelper.GetTranslation(source, "en", "name"));

                Natures.Add(nature.NatureId, nature);
            }
        }

        private void LoadAbilities(string basePath)
        {
            Console.WriteLine("PokeAPI Load Abilities");

            Abilities.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Ability ability = new Ability(source);

                Builder.Instance.FR.Add(ability.KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
                Builder.Instance.EN.Add(ability.KeyName, JsonHelper.GetTranslation(source, "en", "name"));
                Builder.Instance.FR.Add(ability.KeyEffect, JsonHelper.GetEffectEntry(source, "fr"));
                Builder.Instance.EN.Add(ability.KeyEffect, JsonHelper.GetEffectEntry(source, "en"));
                Builder.Instance.FR.Add(ability.KeyFlavor, JsonHelper.GetFlavorEntry(source, "fr"));
                Builder.Instance.EN.Add(ability.KeyFlavor, JsonHelper.GetFlavorEntry(source, "en"));

                Abilities.Add(ability.AbilityId, ability);
            }
        }

        public void LoadMoves()
        {
            Console.WriteLine("PokeAPI Load Moves");

            string basePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Move.BasePath);

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                Move move = new Move(source);

                Builder.Instance.FR.Add(move.KeyName, JsonHelper.GetTranslation(Builder.Instance.cobblemon.FR, string.Format("cobblemon.move.{0}", move.MoveId)));
                Builder.Instance.EN.Add(move.KeyName, JsonHelper.GetTranslation(Builder.Instance.cobblemon.EN, string.Format("cobblemon.move.{0}", move.MoveId)));
                Builder.Instance.FR.Add(move.KeyFlavor, JsonHelper.GetTranslation(Builder.Instance.cobblemon.FR, string.Format("cobblemon.move.{0}.desc", move.MoveId)));
                Builder.Instance.EN.Add(move.KeyFlavor, JsonHelper.GetTranslation(Builder.Instance.cobblemon.EN, string.Format("cobblemon.move.{0}.desc", move.MoveId)));
                Builder.Instance.FR.Add(move.KeyEffect, JsonHelper.GetEffectEntry(source, "fr"));
                Builder.Instance.EN.Add(move.KeyEffect, JsonHelper.GetEffectEntry(source, "en"));

                Moves.Add(move.MoveId, move);
            }
        }

        private void LoadEggGroups(string basePath)
        {
            Console.WriteLine("PokeAPI Load Egg Groups");

            EggGroups.Clear();
            EggGroup eggGroup;

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                eggGroup = new EggGroup(source);

                Builder.Instance.FR.Add(eggGroup.KeyName, JsonHelper.GetTranslation(source, "fr", "name"));
                Builder.Instance.EN.Add(eggGroup.KeyName, JsonHelper.GetTranslation(source, "en", "name"));

                EggGroups.Add(eggGroup.EggGroupId, eggGroup);
            }

            eggGroup = new EggGroup();
            eggGroup.EggGroupId = "amorphous";
            Builder.Instance.FR.Add(eggGroup.KeyName, "informe");
            Builder.Instance.EN.Add(eggGroup.KeyName, "amorphous");
            EggGroups.Add(eggGroup.EggGroupId, eggGroup);
        }

        private void LoadExperienceGroups(string basePath)
        {
            Console.WriteLine("PokeAPI Load Experience Groups");

            ExperienceGroups.Clear();

            foreach (string file in Directory.EnumerateFiles(basePath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                JObject source = JObject.Parse(jsonContent);
                ExperienceGroup experienceGroup = new ExperienceGroup(source);

                Builder.Instance.FR.Add(experienceGroup.KeyDescription, JsonHelper.GetTranslation(source, "fr", "name"));
                Builder.Instance.EN.Add(experienceGroup.KeyDescription, JsonHelper.GetTranslation(source, "en", "name"));

                ExperienceGroups.Add(experienceGroup.ExperienceGroupId, experienceGroup);
            }
        }

        private void LoadSprites(string basePath)
        {
            Console.WriteLine("PokeAPI Load Sprites");

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

        internal void Save()
        {
            string jsonContent = JsonConvert.SerializeObject(this.Generations, Formatting.Indented);
            string filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Generation.FileName);
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

            jsonContent = JsonConvert.SerializeObject(this.EggGroups, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, EggGroup.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.ExperienceGroups, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, ExperienceGroup.FileName);
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.Sprites, Formatting.Indented);
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, Sprite.FileName);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}
