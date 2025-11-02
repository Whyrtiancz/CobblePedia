namespace CobblePedia.ViewModels
{
    using System;
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class SpeciesViewModel : ObservableObject
    {
        [ObservableProperty] private int nationalPokedexNumber;
        [ObservableProperty] private string numero;
        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private string frontDefault;

        public GenerationViewModel Generation { get; private set; }
        public TypeViewModel PrimaryType { get; private set; }
        public TypeViewModel SecondaryType { get; private set; }

        public ObservableCollection<SpriteViewModel> Sprites { get; set; }

        [ObservableProperty] private SpeciesListViewModel preEvolution;

        [ObservableProperty] private int height;
        [ObservableProperty] private int weight;

        [ObservableProperty] private float maleRatio;
        [ObservableProperty] private int eggCycles;

        [ObservableProperty] private int baseExperienceYield;
        [ObservableProperty] private string baseExperienceGroup;
        [ObservableProperty] private int baseFriendship;
        [ObservableProperty] private int catchRate;

        [ObservableProperty] private bool isDynamaxBlocked;


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

        private Pokemon species;

        public SpeciesViewModel(Pokemon source)
        {
            species = source;
            nationalPokedexNumber = source.NationalPokedexNumber;
            numero = "#" + nationalPokedexNumber.ToString("D4");
            name = "-";
            description = "-";

            Generation = new GenerationViewModel(CobblePedia.Pedia.Generations[source.Generation]);

            PrimaryType = new TypeViewModel(CobblePedia.Pedia.Types[source.PrimaryType]);
            if (source.SecondaryType == null)
            {
                SecondaryType = new TypeViewModel();
            }
            else
            {
                SecondaryType = new TypeViewModel(CobblePedia.Pedia.Types[source.SecondaryType]);
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

            height = species.Height;
            weight = species.Weight;

            maleRatio = species.MaleRatio;
            eggCycles = species.EggCycles;

            baseExperienceGroup = species.BaseExperienceGroup;
            baseExperienceYield = species.BaseExperienceYield;
            baseFriendship = species.BaseFriendship;
            catchRate = species.CatchRate;

            isDynamaxBlocked = species.IsDynamaxBlocked;

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

            if (species.PreEvolutionSpeciesId != null)
            {
                if (CobblePedia.Pedia.Pokemon.ContainsKey(species.PreEvolutionSpeciesId))
                {
                    preEvolution = new SpeciesListViewModel(CobblePedia.Pedia.Pokemon[species.PreEvolutionSpeciesId]);
                }
                else
                {
                    Console.WriteLine("--> {0} / {1}", species.SpeciesId, species.PreEvolutionSpeciesId);
                }
            }

            SetLanguage("en");
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[species.KeyName];
                    Description = CobblePedia.Pedia.FR[species.KeyDescription];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[species.KeyName];
                    Description = CobblePedia.Pedia.EN[species.KeyDescription];
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
