namespace CobblePedia.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Windows.Storage;

    using CobblePedia;

    public class CobblePediaModel
    {
        public Dictionary<string, Dictionary<string, string>> Translations { get; private set; }
        public Dictionary<string, Generation> Generations { get; private set; }
        public Dictionary<string, PokemonType> Types { get; private set; }
        public Dictionary<string, Nature> Natures { get; private set; }
        public Dictionary<string, Ability> Abilities { get; private set; }
        public Dictionary<string, Move> Moves { get; private set; }
        public Dictionary<string, EggGroup> EggGroups { get; private set; }
        public Dictionary<string, ExperienceGroup> ExperienceGroups { get; private set; }
        public Dictionary<string, Sprite> Sprites { get; private set; }
        public Dictionary<string, Pokemon> Pokemons { get; private set; }
        public Dictionary<string, Trainer> Trainers { get; private set; }
        public Dictionary<string, Species> PokemonSpecies { get; private set; }
        internal List<Spawn> Spawns { get; private set; }

        private static CobblePediaModel? pedia;


        public static CobblePediaModel Pedia
        {
            get
            {
                if (null == pedia)
                {
                    pedia = new CobblePediaModel();
                }

                return pedia;
            }
        }

        private CobblePediaModel()
        {
            Translations = new Dictionary<string, Dictionary<string, string>>();

            this.Generations = new Dictionary<string, Generation>();
            this.Types = new Dictionary<string, PokemonType>();
            this.Natures = new Dictionary<string, Nature>();
            this.Abilities = new Dictionary<string, Ability>();
            this.Moves = new Dictionary<string, Move>();
            this.EggGroups = new Dictionary<string, EggGroup>();
            this.ExperienceGroups = new Dictionary<string, ExperienceGroup>();
            this.Sprites = new Dictionary<string, Sprite>();
            this.Pokemons = new Dictionary<string, Pokemon>();
            this.Spawns = new List<Spawn>();
            this.Trainers = new Dictionary<string, Trainer>();
        }

        /// <summary>
        /// Chargement des données lorsque la base est déjà alimentée (dataset)
        /// </summary>
        public async Task<bool> Load()
        {
            string uriString = string.Format("ms-appx:///Assets/JSON/{0}.json", "fr");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            string jsonContent = await FileIO.ReadTextAsync(file);
            this.Translations.Add("fr", JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent));

            uriString = string.Format("ms-appx:///Assets/JSON/{0}.json", "en");
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Translations.Add("en", JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent));

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Generation.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Generations = JsonConvert.DeserializeObject<Dictionary<string, Generation>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", PokemonType.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Types = JsonConvert.DeserializeObject<Dictionary<string, PokemonType>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Nature.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Natures = JsonConvert.DeserializeObject<Dictionary<string, Nature>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Ability.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Abilities = JsonConvert.DeserializeObject<Dictionary<string, Ability>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Move.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Moves = JsonConvert.DeserializeObject<Dictionary<string, Move>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", EggGroup.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.EggGroups = JsonConvert.DeserializeObject<Dictionary<string, EggGroup>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", ExperienceGroup.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.ExperienceGroups = JsonConvert.DeserializeObject<Dictionary<string, ExperienceGroup>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Sprite.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Sprites = JsonConvert.DeserializeObject<Dictionary<string, Sprite>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Spawn.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Spawns = JsonConvert.DeserializeObject<List<Spawn>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Pokemon.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Pokemons = JsonConvert.DeserializeObject<Dictionary<string, Pokemon>>(jsonContent);

            uriString = string.Format("ms-appx:///Assets/JSON/{0}", Trainer.FileName);
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            jsonContent = await FileIO.ReadTextAsync(file);
            this.Trainers= JsonConvert.DeserializeObject<Dictionary<string, Trainer>>(jsonContent);

            return true;
        }
    }
}
