namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    using Microsoft.UI.Xaml;

    public partial class TypeExtendedViewModel : ObservableObject
    {
        [ObservableProperty] private string primaryName;
        [ObservableProperty] private string secondaryName;
        [ObservableProperty] private string primaryIcon;
        [ObservableProperty] private string secondaryIcon;

        [ObservableProperty] private Visibility level1Visibility;
        [ObservableProperty] private Visibility level2Visibility;
        [ObservableProperty] private Visibility level3Visibility;
        [ObservableProperty] private Visibility level4Visibility;
        [ObservableProperty] private Visibility level5Visibility;
        [ObservableProperty] private Visibility level6Visibility;

        public ObservableCollection<TypeSimplifiedViewModel> FourfoldDamageTo { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> DoubleDamageTo { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> HalfDamageTo { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> QuarterDamageTo { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> NoDamageTo { get; set; }

        public ObservableCollection<TypeSimplifiedViewModel> NoDamageFrom { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> QuarterDamageFrom { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> HalfDamageFrom { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> DoubleDamageFrom { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> FourfoldDamageFrom { get; set; }

        private PokemonType primaryType;
        private PokemonType? secondaryType;

        public TypeExtendedViewModel(PokemonType type1, PokemonType? type2, bool isPokemonCard)
        {
            primaryType = type1;
            secondaryType = type2;

            primaryIcon = string.Format(Properties.Resources.TypeIconPath, primaryType.TypeId);
            if (secondaryType == null)
            {
                secondaryIcon = string.Format(Properties.Resources.TypeIconPath, "empty");
            }
            else
            {
                secondaryIcon = string.Format(Properties.Resources.TypeIconPath, secondaryType.TypeId);
            }

            FourfoldDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();
            DoubleDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();
            HalfDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();
            QuarterDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();
            NoDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();

            FourfoldDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();
            DoubleDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();
            HalfDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();
            QuarterDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();
            NoDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();

            CalculateForceAndWeakness(primaryType.DoubleDamageTo, primaryType.HalfDamageTo, primaryType.NoDamageTo,
                secondaryType?.DoubleDamageTo, secondaryType?.HalfDamageTo, secondaryType?.NoDamageTo,
                FourfoldDamageTo, DoubleDamageTo, HalfDamageTo, QuarterDamageTo, NoDamageTo);

            CalculateForceAndWeakness(primaryType.DoubleDamageFrom, primaryType.HalfDamageFrom, primaryType.NoDamageFrom,
                secondaryType?.DoubleDamageFrom, secondaryType?.HalfDamageFrom, secondaryType?.NoDamageFrom,
                FourfoldDamageFrom, DoubleDamageFrom, HalfDamageFrom, QuarterDamageFrom, NoDamageFrom);

            level1Visibility = FourfoldDamageTo.Count > 0 || NoDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            level2Visibility = DoubleDamageTo.Count > 0 || QuarterDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            level3Visibility = HalfDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            level4Visibility = HalfDamageTo.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            level5Visibility = QuarterDamageTo.Count > 0 || DoubleDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            level6Visibility = NoDamageTo.Count > 0 || FourfoldDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (isPokemonCard)
            {
                level1Visibility = NoDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                level2Visibility = QuarterDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                level3Visibility = HalfDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                level4Visibility = Visibility.Collapsed;
                level5Visibility = DoubleDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                level6Visibility = FourfoldDamageFrom.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }

            if (FourfoldDamageTo.Count == 0)
            {
                FourfoldDamageTo.Add(new TypeSimplifiedViewModel());
            }
            if (DoubleDamageTo.Count == 0)
            {
                DoubleDamageTo.Add(new TypeSimplifiedViewModel());
            }
            if (HalfDamageTo.Count == 0)
            {
                HalfDamageTo.Add(new TypeSimplifiedViewModel());
            }
            if (QuarterDamageTo.Count == 0)
            {
                QuarterDamageTo.Add(new TypeSimplifiedViewModel());
            }
            if (NoDamageTo.Count == 0)
            {
                NoDamageTo.Add(new TypeSimplifiedViewModel());
            }

            if (FourfoldDamageFrom.Count == 0)
            {
                FourfoldDamageFrom.Add(new TypeSimplifiedViewModel());
            }
            if (DoubleDamageFrom.Count == 0)
            {
                DoubleDamageFrom.Add(new TypeSimplifiedViewModel());
            }
            if (HalfDamageFrom.Count == 0)
            {
                HalfDamageFrom.Add(new TypeSimplifiedViewModel());
            }
            if (QuarterDamageFrom.Count == 0)
            {
                QuarterDamageFrom.Add(new TypeSimplifiedViewModel());
            }
            if (NoDamageFrom.Count == 0)
            {
                NoDamageFrom.Add(new TypeSimplifiedViewModel());
            }

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        private void CalculateForceAndWeakness(List<string> primaryDouble, List<string> primaryHalf, List<string> primaryNo,
                                               List<string>? secondaryDouble, List<string>? secondaryHalf, List<string>? secondaryNo,
                                               ObservableCollection<TypeSimplifiedViewModel> fourths, ObservableCollection<TypeSimplifiedViewModel> doubles,
                                               ObservableCollection<TypeSimplifiedViewModel> halfs, ObservableCollection<TypeSimplifiedViewModel> quarters,
                                               ObservableCollection<TypeSimplifiedViewModel> nos)
        {
            Dictionary<string, float> calculation = new Dictionary<string, float>();

            foreach (string item in primaryDouble)
            {
                calculation.Add(item, 2.0f);
            }
            foreach (string item in primaryHalf)
            {
                calculation.Add(item, 0.5f);
            }
            foreach (string item in primaryNo)
            {
                calculation.Add(item, 0.0f);
            }

            if (secondaryDouble != null)
            {
                foreach (string item in secondaryDouble)
                {
                    if (calculation.ContainsKey(item))
                    {
                        calculation[item] = calculation[item] * 2.0f;
                    }
                    else
                    {
                        calculation.Add(item, 2.0f);
                    }
                }
                foreach (string item in secondaryHalf)
                {
                    if (calculation.ContainsKey(item))
                    {
                        calculation[item] = calculation[item] * 0.5f;
                    }
                    else
                    {
                        calculation.Add(item, 0.5f);
                    }
                }
                foreach (string item in secondaryNo)
                {
                    if (calculation.ContainsKey(item))
                    {
                        calculation[item] = calculation[item] * 0.0f;
                    }
                    else
                    {
                        calculation.Add(item, 0.0f);
                    }
                }
            }

            foreach (string item in calculation.Keys)
            {
                switch (calculation[item])
                {
                    case 0.0f:
                        nos.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
                        break;
                    case 0.25f:
                        quarters.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
                        break;
                    case 0.5f:
                        halfs.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
                        break;
                    case 2.0f:
                        doubles.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
                        break;
                    case 4.0f:
                        fourths.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
                        break;
                    default:
                        break;
                }
            }
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    PrimaryName = CobblePedia.Pedia.FR[primaryType.KeyName];
                    if (secondaryType != null)
                    {
                        SecondaryName = CobblePedia.Pedia.FR[secondaryType.KeyName];
                    }
                    break;
                default:
                    PrimaryName = CobblePedia.Pedia.EN[primaryType.KeyName];
                    if (secondaryType != null)
                    {
                        SecondaryName = CobblePedia.Pedia.EN[secondaryType.KeyName];
                    }
                    break;
            }

            foreach (TypeSimplifiedViewModel item in FourfoldDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in DoubleDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in HalfDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in QuarterDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in NoDamageTo)
            {
                item.SetLanguage(language);
            }

            foreach (TypeSimplifiedViewModel item in NoDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in QuarterDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in HalfDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in DoubleDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in FourfoldDamageFrom)
            {
                item.SetLanguage(language);
            }
        }
    }
}
