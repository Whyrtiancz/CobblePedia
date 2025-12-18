namespace CobblePedia.ViewModels
{
    using System;
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

        public TypeExtendedViewModel(List<string> pokemontypes, bool isPokemonCard)
        {
            List<PokemonType> types = new List<PokemonType>();
            Dictionary<string, float> calculationTo = new Dictionary<string, float>();
            Dictionary<string, float> calculationFrom = new Dictionary<string, float>();

            foreach (string item in pokemontypes)
            {
                types.Add(CobblePediaModel.Pedia.Types[item]);
            }

            foreach (PokemonType item in types) 
            {
                foreach (string subitem in item.DoubleDamageTo)
                {
                    if (calculationTo.ContainsKey(subitem))
                    {
                        calculationTo[subitem] = calculationTo[subitem] * 2.0f;
                    }
                    else
                    {
                        calculationTo.Add(subitem, 2.0f);
                    }
                }
                foreach (string subitem in item.HalfDamageTo)
                {
                    if (calculationTo.ContainsKey(subitem))
                    {
                        calculationTo[subitem] = calculationTo[subitem] * 0.5f;
                    }
                    else
                    {
                        calculationTo.Add(subitem, 0.5f);
                    }
                }
                foreach (string subitem in item.NoDamageTo)
                {
                    if (calculationTo.ContainsKey(subitem))
                    {
                        calculationTo[subitem] = calculationTo[subitem] * 0.0f;
                    }
                    else
                    {
                        calculationTo.Add(subitem, 0.0f);
                    }
                }
                foreach (string subitem in item.DoubleDamageFrom)
                {
                    if (calculationFrom.ContainsKey(subitem))
                    {
                        calculationFrom[subitem] = calculationFrom[subitem] * 2.0f;
                    }
                    else
                    {
                        calculationFrom.Add(subitem, 2.0f);
                    }
                }
                foreach (string subitem in item.HalfDamageFrom)
                {
                    if (calculationFrom.ContainsKey(subitem))
                    {
                        calculationFrom[subitem] = calculationFrom[subitem] * 0.5f;
                    }
                    else
                    {
                        calculationFrom.Add(subitem, 0.5f);
                    }
                }
                foreach (string subitem in item.NoDamageFrom)
                {
                    if (calculationFrom.ContainsKey(subitem))
                    {
                        calculationFrom[subitem] = calculationFrom[subitem] * 0.0f;
                    }
                    else
                    {
                        calculationFrom.Add(subitem, 0.0f);
                    }
                }
            }

            FourfoldDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            DoubleDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            HalfDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            QuarterDamageTo = new ObservableCollection<SearchableObjectViewModel>();
            NoDamageTo = new ObservableCollection<SearchableObjectViewModel>();

            foreach (string item in calculationTo.Keys)
            {
                switch (calculationTo[item])
                {
                    case 0.0f:
                        NoDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 0.25f:
                        QuarterDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 0.5f:
                        HalfDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 2.0f:
                        DoubleDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 4.0f:
                        FourfoldDamageTo.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    default:
                        break;
                }
            }

            FourfoldDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            DoubleDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            HalfDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            QuarterDamageFrom = new ObservableCollection<SearchableObjectViewModel>();
            NoDamageFrom = new ObservableCollection<SearchableObjectViewModel>();

            foreach (string item in calculationFrom.Keys)
            {
                switch (calculationFrom[item])
                {
                    case 0.0f:
                        NoDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 0.25f:
                        QuarterDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 0.5f:
                        HalfDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 2.0f:
                        DoubleDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    case 4.0f:
                        FourfoldDamageFrom.Add(CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
                        break;
                    default:
                        break;
                }
            }

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
        }

        public TypeExtendedViewModel(string pokemontype, bool isPokemonCard) :
            this(new List<string> { pokemontype }, isPokemonCard)
        {
        }
    }
}
