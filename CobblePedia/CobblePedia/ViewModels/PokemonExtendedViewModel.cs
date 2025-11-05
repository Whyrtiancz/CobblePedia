namespace CobblePedia.ViewModels
{
    using System;
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class PokemonExtendedViewModel : ObservableObject
    {

        #region General
        [ObservableProperty] private bool isImplemented;
        [ObservableProperty] private bool isForm;
        [ObservableProperty] private int nationalPokedexNumber;
        [ObservableProperty] private string numero;
        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private string frontDefault;
        public GenerationViewModel Generation { get; private set; }
        public TypeSimplifiedViewModel PrimaryType { get; private set; }
        public TypeSimplifiedViewModel SecondaryType { get; private set; }
        public ObservableCollection<SpriteViewModel> Sprites { get; set; }
        #endregion General

        #region Physique
        [ObservableProperty] private string height;
        [ObservableProperty] private string heightIcon;
        [ObservableProperty] private string weight;
        [ObservableProperty] private string weightIcon;
        #endregion Physique

        #region Reproduction
        [ObservableProperty] private float maleRatio;
        [ObservableProperty] private string ratio;
        [ObservableProperty] private int eggCycles;
        [ObservableProperty] private string eggGroups;
        #endregion Reproduction

        #region Evolution
        [ObservableProperty] private PokemonSimplifiedViewModel preEvolution;
        public ObservableCollection<EvolutionViewModel> Evolutions { get; set; }
        #endregion Evolution

        #region Formes
        #endregion Formes

        #region Entrainement et capture
        [ObservableProperty] private int baseExperienceYield;
        [ObservableProperty] private string baseExperienceGroup;
        [ObservableProperty] private int baseFriendship;
        [ObservableProperty] private int catchRate;
        #endregion Entrainement et capture

        #region Statistiques
        [ObservableProperty] private int baseHP;
        [ObservableProperty] private int baseAttack;
        [ObservableProperty] private int baseDefence;
        [ObservableProperty] private int baseSpecialAttack;
        [ObservableProperty] private int baseSpecialDefence;
        [ObservableProperty] private int baseSpeed;
        [ObservableProperty] private int baseTotal;

        [ObservableProperty] private int evHP;
        [ObservableProperty] private int evAttack;
        [ObservableProperty] private int evDefence;
        [ObservableProperty] private int evSpecialAttack;
        [ObservableProperty] private int evSpecialDefence;
        [ObservableProperty] private int evSpeed;
        #endregion Statistiques

        [ObservableProperty] private bool isDynamaxBlocked;


        private Pokemon species;

        public PokemonExtendedViewModel(Pokemon source)
        {
            species = source;
            #region General
            isImplemented = species.Implemented;
            isForm = species.IsForm;
            nationalPokedexNumber = source.NationalPokedexNumber;
            numero = "#" + nationalPokedexNumber.ToString("D4");
            name = "-";
            description = "-";
            Generation = new GenerationViewModel(CobblePedia.Pedia.Generations[source.Generation]);
            PrimaryType = new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[source.PrimaryType]);
            if (source.SecondaryType == null)
            {
                SecondaryType = new TypeSimplifiedViewModel();
            }
            else
            {
                SecondaryType = new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[source.SecondaryType]);
            }

            frontDefault = string.Format(Properties.Resources.PokemonPicturePath, source.Picture.FrontDefault);
            Sprites = new ObservableCollection<SpriteViewModel>();
            if (source.Picture.FrontFemale != null)
            {
                Sprites.Add(new SpriteViewModel(SpriteViewModel.SpriteCategory.Male, source.Picture.FrontDefault, source.Picture.BackDefault));
                Sprites.Add(new SpriteViewModel(SpriteViewModel.SpriteCategory.Female, source.Picture.FrontFemale, source.Picture.BackFemale));
            }
            else
            {
                Sprites.Add(new SpriteViewModel(SpriteViewModel.SpriteCategory.Default, source.Picture.FrontDefault, source.Picture.BackDefault));
            }
            if (source.Picture.FrontFemaleShiny != null)
            {
                Sprites.Add(new SpriteViewModel(SpriteViewModel.SpriteCategory.MaleShiny, source.Picture.FrontShiny, source.Picture.BackShiny));
                Sprites.Add(new SpriteViewModel(SpriteViewModel.SpriteCategory.FemaleShiny, source.Picture.FrontFemaleShiny, source.Picture.BackFemaleShiny));
            }
            else
            {
                Sprites.Add(new SpriteViewModel(SpriteViewModel.SpriteCategory.DefaultShiny, source.Picture.FrontShiny, source.Picture.BackShiny));
            }
            #endregion General

            #region Physique
            height = string.Format("{0:F1} m.", species.Height / 10.0);
            heightIcon = string.Format(Properties.Resources.PokemonHeightPicture, GetHeightIndex(species.Height));
            weight = string.Format("{0:F1} kg.", species.Weight / 10.0);
            weightIcon = string.Format(Properties.Resources.PokemonWeightPicture, GetWeightIndex(species.Weight));
            #endregion Physique

            #region Reproduction
            ratio = string.Format("{0:P}", species.MaleRatio);
            maleRatio = species.MaleRatio;
            eggCycles = species.EggCycles;
            #endregion Reproduction

            #region Evolution
            preEvolution = new PokemonSimplifiedViewModel();
            if (species.PreEvolutionSpeciesId != null)
            {
                if (CobblePedia.Pedia.Pokemon.ContainsKey(species.PreEvolutionSpeciesId))
                {
                    preEvolution = new PokemonSimplifiedViewModel(CobblePedia.Pedia.Pokemon[species.PreEvolutionSpeciesId]);
                }
                else
                {
                    Console.WriteLine("--> {0} / {1}", species.SpeciesId, species.PreEvolutionSpeciesId);
                }
            }
            Evolutions = new ObservableCollection<EvolutionViewModel>();
            foreach (Evolution evolution in species.Evolutions)
            {
                Evolutions.Add(new EvolutionViewModel(evolution));
            }
            #endregion Evolution

            #region Formes
            #endregion Formes

            #region Entrainement et capture
            //    [ObservableProperty] private int baseExperienceYield;
            //[ObservableProperty] private string baseExperienceGroup;
            //[ObservableProperty] private int baseFriendship;
            //[ObservableProperty] private int catchRate;
            #endregion Entrainement et capture

            #region Statistiques
            baseExperienceGroup = species.BaseExperienceGroup;
            baseExperienceYield = species.BaseExperienceYield;
            baseFriendship = species.BaseFriendship;
            catchRate = species.CatchRate;

            baseAttack = species.BaseAttack;
            baseDefence = species.BaseDefence;
            baseHP = species.BaseHP;
            baseSpecialAttack = species.BaseSpecialAttack;
            baseSpecialDefence = species.BaseSpecialDefence;
            baseSpeed = species.BaseSpeed;
            BaseTotal = species.BaseTotal;

            evAttack = species.EvAttack;
            evDefence = species.EvDefence;
            evHP = species.EvHP;
            evSpecialAttack = species.EvSpecialAttack;
            evSpecialDefence = species.EvSpecialDefence;
            evSpeed = species.EvSpeed;
            #endregion Statistiques

            isDynamaxBlocked = species.IsDynamaxBlocked;

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        private int GetWeightIndex(int weight) => weight switch
        {
            < 320 => 1,
            >= 320 and < 700 => 2,
            >= 700 and < 900 => 3,
            >= 900 and < 3200 => 4,
            >= 3200 => 5
        };

        private int GetHeightIndex(int height) => height switch
        {
            < 10 => 1,
            >= 10 and < 16 => 2,
            >= 16 and < 19 => 3,
            >= 19 and < 50 => 4,
            >= 50 => 5
        };


        internal void SetLanguage(string language)
        {
            EggGroups = string.Join(", ", species.EggGroups);
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[species.KeyName];
                    Description = CobblePedia.Pedia.FR[species.KeyDescription];
                    foreach (string item in species.EggGroups)
                    {
                        EggGroups = EggGroups.Replace(item, CobblePedia.Pedia.FR[string.Format(Properties.Resources.PokemonEggGroup, item)]);
                    }
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[species.KeyName];
                    Description = CobblePedia.Pedia.EN[species.KeyDescription];
                    foreach (string item in species.EggGroups)
                    {
                        EggGroups = EggGroups.Replace(item, CobblePedia.Pedia.EN[string.Format(Properties.Resources.PokemonEggGroup, item)]);
                    }
                    break;
            }

            Generation.SetLanguage(language);
            PrimaryType.SetLanguage(language);
            SecondaryType.SetLanguage(language);

            foreach (SpriteViewModel item in Sprites)
            {
                item.SetLanguage(language);
            }

            PreEvolution?.SetLanguage(language);
        }
    }
}
