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

        public ObservableCollection<SearchableObjectViewModel> FourfoldDamageTo { get; set; }
        public ObservableCollection<SearchableObjectViewModel> DoubleDamageTo { get; set; }
        public ObservableCollection<SearchableObjectViewModel> HalfDamageTo { get; set; }
        public ObservableCollection<SearchableObjectViewModel> QuarterDamageTo { get; set; }
        public ObservableCollection<SearchableObjectViewModel> NoDamageTo { get; set; }

        public ObservableCollection<SearchableObjectViewModel> NoDamageFrom { get; set; }
        public ObservableCollection<SearchableObjectViewModel> QuarterDamageFrom { get; set; }
        public ObservableCollection<SearchableObjectViewModel> HalfDamageFrom { get; set; }
        public ObservableCollection<SearchableObjectViewModel> DoubleDamageFrom { get; set; }
        public ObservableCollection<SearchableObjectViewModel> FourfoldDamageFrom { get; set; }

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

            FourfoldDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            DoubleDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            HalfDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            QuarterDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            NoDamageTo = new ObservableCollection<SearchableObjectViewModel>();

            FourfoldDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            DoubleDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            HalfDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            QuarterDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            NoDamageFrom = new ObservableCollection<SearchableObjectViewModel>();

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
                FourfoldDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (DoubleDamageTo.Count == 0)
            {
                DoubleDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (HalfDamageTo.Count == 0)
            {
                HalfDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (QuarterDamageTo.Count == 0)
            {
                QuarterDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (NoDamageTo.Count == 0)
            {
                NoDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }

            if (FourfoldDamageFrom.Count == 0)
            {
                FourfoldDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (DoubleDamageFrom.Count == 0)
            {
                DoubleDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (HalfDamageFrom.Count == 0)
            {
                HalfDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (QuarterDamageFrom.Count == 0)
            {
                QuarterDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }
            if (NoDamageFrom.Count == 0)
            {
                NoDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(string.Empty, string.Empty));
            }

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        private void CalculateForceAndWeakness(List<string> primaryDouble, List<string> primaryHalf, List<string> primaryNo,
                                               List<string>? secondaryDouble, List<string>? secondaryHalf, List<string>? secondaryNo,
                                               ObservableCollection<SearchableObjectViewModel> fourths, ObservableCollection<SearchableObjectViewModel> doubles,
                                               ObservableCollection<SearchableObjectViewModel> halfs, ObservableCollection<SearchableObjectViewModel> quarters,
                                               ObservableCollection<SearchableObjectViewModel> nos)
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
                        nos.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 0.25f:
                        quarters.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 0.5f:
                        halfs.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 2.0f:
                        doubles.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 4.0f:
                        fourths.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
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

            //foreach (SearchableObjectViewModel item in FourfoldDamageTo)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in DoubleDamageTo)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in HalfDamageTo)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in QuarterDamageTo)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in NoDamageTo)
            //{
            //    item.SetLanguage(language);
            //}

            //foreach (SearchableObjectViewModel item in NoDamageFrom)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in QuarterDamageFrom)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in HalfDamageFrom)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in DoubleDamageFrom)
            //{
            //    item.SetLanguage(language);
            //}
            //foreach (SearchableObjectViewModel item in FourfoldDamageFrom)
            //{
            //    item.SetLanguage(language);
            //}
        }
    }
}
