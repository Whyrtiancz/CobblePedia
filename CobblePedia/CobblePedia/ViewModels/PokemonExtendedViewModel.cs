namespace CobblePedia.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Reflection;

    using CobblePedia.Helpers;
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    using Microsoft.UI.Xaml.Media;

    using Windows.UI;

    internal partial class PokemonExtendedViewModel : ObservableObject
    {

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

        [ObservableProperty] private string height;
        [ObservableProperty] private string heightIcon;
        [ObservableProperty] private string weight;
        [ObservableProperty] private string weightIcon;

        [ObservableProperty] private bool isBreedable;
        [ObservableProperty] private Brush background;
        [ObservableProperty] private Brush foreground;
        [ObservableProperty] private float maleRatio;
        [ObservableProperty] private string ratio;
        [ObservableProperty] private int eggCycles;
        public ObservableCollection<EggGroupViewModel> EggGroups { get; private set; }
        [ObservableProperty] private EggGroupViewModel primaryEggGroup;
        [ObservableProperty] private EggGroupViewModel secondaryEggGroup;

        [ObservableProperty] private string frontDefault;
        [ObservableProperty] private SearchableObjectViewModel preEvolution;
        [ObservableProperty] private EvolutionViewModel preEvolutionCondition;
        public ObservableCollection<EvolutionViewModel> Evolutions { get; set; }

        public ObservableCollection<SearchableObjectViewModel> Forms { get; set; }

        [ObservableProperty] private int baseExperienceYield;
        [ObservableProperty] private string baseExperienceGroup;
        [ObservableProperty] private int baseFriendship;
        [ObservableProperty] private int catchRate;

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

        public ObservableCollection<MoveViewModel> LeveledMoves { get; set; }
        public ObservableCollection<MoveViewModel> TMMoves { get; set; }
        public ObservableCollection<MoveViewModel> TutorMoves { get; set; }

        public ObservableCollection<DropViewModel> Drops { get; set; }

        public ObservableCollection<AbilityViewModel> Abilities { get; set; }

        public ObservableCollection<SpawnConditionViewModel> Spawns { get; set; }

        [ObservableProperty] private bool isDynamaxBlocked;

        private string keyName;
        private string keyDescription;

        public PokemonExtendedViewModel(Pokemon source)
        {
            keyName = source.KeyName;
            keyDescription = source.KeyDescription;
            isImplemented = source.Implemented;
            isForm = source.IsForm;
            nationalPokedexNumber = source.NationalPokedexNumber;
            numero = "#" + nationalPokedexNumber.ToString("D4");
            name = string.Empty;
            description = string.Empty;
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

            height = string.Format("{0:F1} m.", source.Height / 10.0);
            heightIcon = string.Format(Properties.Resources.PokemonHeightPicture, GetHeightIndex(source.Height));
            weight = string.Format("{0:F1} kg.", source.Weight / 10.0);
            weightIcon = string.Format(Properties.Resources.PokemonWeightPicture, GetWeightIndex(source.Weight));

            IsBreedable = source.EggGroups is not null && source.EggGroups.Count > 0 && source.EggGroups[0] != "no-eggs";
            EggGroups = new ObservableCollection<EggGroupViewModel>();
            foreach (string item in source.EggGroups)
            {
                EggGroups.Add(new EggGroupViewModel(CobblePediaModel.Pedia.EggGroups[item]));
            }

            switch (source.EggGroups.Count)
            {
                case 1:
                    primaryEggGroup = CobblePediaViewModel.Model.eggGroups[source.EggGroups[0]];
                    secondaryEggGroup = new EggGroupViewModel();
                    break;
                case 2:
                    primaryEggGroup = CobblePediaViewModel.Model.eggGroups[source.EggGroups[0]];
                    secondaryEggGroup = CobblePediaViewModel.Model.eggGroups[source.EggGroups[1]];
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
                ratio = string.Format("{0:P1}", source.MaleRatio);
                maleRatio = source.MaleRatio;
            }
            else
            {
                foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA9, 0xA9, 0xA9));
                background = foreground;
                ratio = primaryEggGroup.Name;
                maleRatio = 0;
            }
            eggCycles = source.EggCycles;

            frontDefault = string.Format(Properties.Resources.PokemonPicturePath, source.Picture.FrontDefault);
            preEvolution = CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty);
            preEvolutionCondition = new EvolutionViewModel();
            if (source.PreEvolutionSpeciesId != null)
            {
                if (CobblePediaModel.Pedia.Pokemons.ContainsKey(source.PreEvolutionSpeciesId))
                {
                    preEvolution = CobblePediaViewModel.Model.GetSearchableObjectViewModel(source.PreEvolutionSpeciesId, "@pokemon");
                    preEvolutionCondition = new EvolutionViewModel(CobblePediaModel.Pedia.Pokemons[source.PreEvolutionSpeciesId].GetEvolutionTo(source.SpeciesId)[0]);
                }
            }
            Evolutions = new ObservableCollection<EvolutionViewModel>();
            foreach (Evolution evolution in source.Evolutions)
            {
                Evolutions.Add(new EvolutionViewModel(evolution));
            }
            Forms = new ObservableCollection<SearchableObjectViewModel>();
            foreach (string item in source.Forms)
            {
                Forms.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@pokemon"));
            }

            baseExperienceYield = source.BaseExperienceYield;
            baseExperienceGroup = source.BaseExperienceGroup;
            baseFriendship = source.BaseFriendship;
            catchRate = source.CatchRate;

            baseExperienceGroup = source.BaseExperienceGroup;
            baseExperienceYield = source.BaseExperienceYield;
            baseFriendship = source.BaseFriendship;
            catchRate = source.CatchRate;

            baseAttack = source.BaseAttack;
            baseDefence = source.BaseDefence;
            baseHP = source.BaseHP;
            baseSpecialAttack = source.BaseSpecialAttack;
            baseSpecialDefence = source.BaseSpecialDefence;
            baseSpeed = source.BaseSpeed;
            BaseTotal = source.BaseTotal;

            evAttack = source.EvAttack;
            evDefence = source.EvDefence;
            evHP = source.EvHP;
            evSpecialAttack = source.EvSpecialAttack;
            evSpecialDefence = source.EvSpecialDefence;
            evSpeed = source.EvSpeed;

            LeveledMoves = new ObservableCollection<MoveViewModel>();
            TMMoves = new ObservableCollection<MoveViewModel>();
            TutorMoves = new ObservableCollection<MoveViewModel>();

            foreach (PokemonMove item in source.EggMoves)
            {
                LeveledMoves.Add(new MoveViewModel(CobblePediaModel.Pedia.Moves[item.MoveId], MoveViewModel.EMoveLearnType.Egg));
            }
            foreach (PokemonMove item in source.LeveledMoves)
            {
                LeveledMoves.Add(new MoveViewModel(CobblePediaModel.Pedia.Moves[item.MoveId], item.Level));
            }
            foreach (PokemonMove item in source.TMMoves)
            {
                TMMoves.Add(new MoveViewModel(CobblePediaModel.Pedia.Moves[item.MoveId], MoveViewModel.EMoveLearnType.CT_CM));
            }
            foreach (PokemonMove item in source.TutorMoves)
            {
                TutorMoves.Add(new MoveViewModel(CobblePediaModel.Pedia.Moves[item.MoveId], MoveViewModel.EMoveLearnType.Tutor));
            }

            Drops = new ObservableCollection<DropViewModel>();
            foreach (Drop item in source.Drops)
            {
                Drops.Add(new DropViewModel(item));
            }

            Abilities = new ObservableCollection<AbilityViewModel>();
            foreach (PokemonAbility item in source.Abilities)
            {
                Abilities.Add(new AbilityViewModel(item));
            }

            Spawns = new ObservableCollection<SpawnConditionViewModel>();
            foreach (Spawn item in source.Spawns)
            {
                Spawns.Add(new SpawnConditionViewModel(item));
            }

            isDynamaxBlocked = source.IsDynamaxBlocked;

            SetLanguage();
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


        internal void SetLanguage()
        {
            Name = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][keyName];
            Description = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][keyDescription];

            Generation.SetLanguage();

            foreach (EggGroupViewModel item in EggGroups)
            {
                item.SetLanguage();
            }
            foreach (SpriteViewModel item in Sprites)
            {
                item.SetLanguage();
            }
            foreach (AbilityViewModel item in Abilities)
            {
                item.SetLanguage();
            }
            foreach (DropViewModel item in Drops)
            {
                item.SetLanguage();
            }
            foreach (SpawnConditionViewModel item in Spawns)
            {
                item.SetLanguage();
            }

            if (!IsBreedable)
            {
                Ratio = EggGroups[0].Name;
            }
        }
    }
}