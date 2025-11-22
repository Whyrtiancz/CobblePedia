namespace CobblePedia.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Reflection;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    using Microsoft.UI.Xaml.Media;

    using Windows.UI;

    internal partial class PokemonExtendedViewModel : ObservableObject
    {

        #region General
        [ObservableProperty] private bool isImplemented;
        [ObservableProperty] private bool isForm;
        [ObservableProperty] private int nationalPokedexNumber;
        [ObservableProperty] private string numero;
        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        public TypeExtendedViewModel TypeEfficiency { get; private set; }
        public GenerationViewModel Generation { get; private set; }
        public ObservableCollection<SpriteViewModel> Sprites { get; private set; }
        public ObservableCollection<SearchableObjectViewModel> Types { get; private set; }
        #endregion General

        #region Physique
        [ObservableProperty] private string height;
        [ObservableProperty] private string heightIcon;
        [ObservableProperty] private string weight;
        [ObservableProperty] private string weightIcon;
        #endregion Physique

        #region Reproduction
        [ObservableProperty] private bool isBreedable;
        [ObservableProperty] private Brush background;
        [ObservableProperty] private Brush foreground;
        [ObservableProperty] private float maleRatio;
        [ObservableProperty] private string ratio;
        [ObservableProperty] private int eggCycles;
        [ObservableProperty] private EggGroupViewModel primaryEggGroup;
        [ObservableProperty] private EggGroupViewModel secondaryEggGroup;
        #endregion Reproduction

        #region Evolution
        [ObservableProperty] private string frontDefault;
        [ObservableProperty] private SearchableObjectViewModel preEvolution;
        [ObservableProperty] private EvolutionViewModel preEvolutionCondition;
        public ObservableCollection<EvolutionViewModel> Evolutions { get; set; }
        #endregion Evolution

        #region Formes
        public ObservableCollection<SearchableObjectViewModel> Forms { get; set; }
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

        #region Attaques
        public ObservableCollection<MoveViewModel> LeveledMoves { get; set; }
        public ObservableCollection<MoveViewModel> TMMoves { get; set; }
        public ObservableCollection<MoveViewModel> TutorMoves { get; set; }
        #endregion Attaques

        #region Drops
        public ObservableCollection<DropViewModel> Drops { get; set; }
        #endregion Drops

        #region Capacités
        public ObservableCollection<AbilityViewModel> Abilities { get; set; }
        #endregion Capacités

        #region Drops
        public ObservableCollection<SpawnConditionViewModel> Spawns { get; set; }
        #endregion Drops

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
            Generation = CobblePediaViewModel.Model.generations[source.Generation];
            Types = new ObservableCollection<SearchableObjectViewModel>();
            foreach (string itemId in source.Types)
            {
                Types.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(itemId, "@type"));
            }
            TypeEfficiency = new TypeExtendedViewModel(source.Types, true);

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
            IsBreedable = species.EggGroups is not null && species.EggGroups.Count > 0 && species.EggGroups[0] != "no-eggs";
            switch (species.EggGroups.Count)
            {
                case 1:
                    primaryEggGroup = CobblePediaViewModel.Model.eggGroups[species.EggGroups[0]];
                    secondaryEggGroup = new EggGroupViewModel();
                    break;
                case 2:
                    primaryEggGroup = CobblePediaViewModel.Model.eggGroups[species.EggGroups[0]];
                    secondaryEggGroup = CobblePediaViewModel.Model.eggGroups[species.EggGroups[1]];
                    break;
                default:
                    primaryEggGroup = new EggGroupViewModel();
                    secondaryEggGroup = primaryEggGroup;
                    break;
            }

            if (IsBreedable)
            {
                foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x8B));
                background = new SolidColorBrush(Color.FromArgb(0xFF, 0x8B, 0x00, 0x00));
                ratio = string.Format("{0:P1}", species.MaleRatio);
                maleRatio = species.MaleRatio;
            }
            else
            {
                foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA9, 0xA9, 0xA9));
                background = foreground;
                ratio = primaryEggGroup.Name;
                maleRatio = 0;
            }
            eggCycles = species.EggCycles;
            #endregion Reproduction

            #region Evolution
            frontDefault = string.Format(Properties.Resources.PokemonPicturePath, species.Picture.FrontDefault);
            preEvolution = CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty);
            preEvolutionCondition = new EvolutionViewModel();
            if (species.PreEvolutionSpeciesId != null)
            {
                if (CobblePedia.Pedia.Pokemon.ContainsKey(species.PreEvolutionSpeciesId))
                {
                    preEvolution = CobblePediaViewModel.Model.GetSearchableObjectViewModel(species.PreEvolutionSpeciesId, "@pokemon");
                    preEvolutionCondition = new EvolutionViewModel(CobblePedia.Pedia.Pokemon[species.PreEvolutionSpeciesId].GetEvolutionTo(species.SpeciesId)[0]);
                }
            }
            Evolutions = new ObservableCollection<EvolutionViewModel>();
            foreach (Evolution evolution in species.Evolutions)
            {
                Evolutions.Add(new EvolutionViewModel(evolution));
            }
            Forms = new ObservableCollection<SearchableObjectViewModel>();
            foreach (string item in species.Forms)
            {
                Forms.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@pokemon"));
            }
            #endregion Evolution

            #region Formes
            #endregion Formes

            #region Entrainement et capture
            baseExperienceYield = species.BaseExperienceYield;
            baseExperienceGroup = species.BaseExperienceGroup;
            baseFriendship = species.BaseFriendship;
            catchRate = species.CatchRate;
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

            #region Attaques
            LeveledMoves = new ObservableCollection<MoveViewModel>();
            TMMoves = new ObservableCollection<MoveViewModel>();
            TutorMoves = new ObservableCollection<MoveViewModel>();

            foreach (PokemonMove item in species.EggMoves)
            {
                LeveledMoves.Add(new MoveViewModel(CobblePedia.Pedia.Moves[item.MoveId], MoveViewModel.EMoveLearnType.Egg));
            }
            foreach (PokemonMove item in species.LeveledMoves)
            {
                LeveledMoves.Add(new MoveViewModel(CobblePedia.Pedia.Moves[item.MoveId], item.Level));
            }
            foreach (PokemonMove item in species.TMMoves)
            {
                TMMoves.Add(new MoveViewModel(CobblePedia.Pedia.Moves[item.MoveId], MoveViewModel.EMoveLearnType.CT_CM));
            }
            foreach (PokemonMove item in species.TutorMoves)
            {
                TutorMoves.Add(new MoveViewModel(CobblePedia.Pedia.Moves[item.MoveId], MoveViewModel.EMoveLearnType.Tutor));
            }
            #endregion Attaques

            #region Drops
            Drops = new ObservableCollection<DropViewModel>();
            foreach (Drop item in species.Drops)
            {
                Drops.Add(new DropViewModel(item));
            }
            #endregion Drops

            #region Capacités
            Abilities = new ObservableCollection<AbilityViewModel>();
            foreach (PokemonAbility item in species.Abilities)
            {
                Abilities.Add(new AbilityViewModel(item));
            }
            #endregion Capacités

            #region Spawn
            Spawns = new ObservableCollection<SpawnConditionViewModel>();
            foreach (Spawn item in species.Spawns)
            {
                Spawns.Add(new SpawnConditionViewModel(item));
            }
            #endregion Spawn
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
            //PrimaryType.SetLanguage(language);
            //SecondaryType.SetLanguage(language);
            primaryEggGroup.SetLanguage(language);
            secondaryEggGroup.SetLanguage(language);

            foreach (SpriteViewModel item in Sprites)
            {
                item.SetLanguage(language);
            }
            //foreach (MoveViewModel item in LeveledMoves)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (MoveViewModel item in TMMoves)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (MoveViewModel item in TutorMoves)
            //{
            //    item.SetLanguage(language);
            //}
            foreach (AbilityViewModel item in Abilities)
            {
                item.SetLanguage(language);
            }
            foreach (DropViewModel item in Drops)
            {
                item.SetLanguage(language);
            }
            foreach (SpawnConditionViewModel item in Spawns)
            {
                item.SetLanguage(language);
            }

            if (!IsBreedable)
            {
                Ratio = primaryEggGroup.Name;
            }

            //PreEvolution?.SetLanguage(language);
            //PreEvolutionCondition?.SetLanguage(language);
        }
    }
}