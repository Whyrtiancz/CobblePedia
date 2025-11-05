namespace CobblePedia.ViewModels
{
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class TypeExtendedViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;
        [ObservableProperty] private string largeIcon;
        [ObservableProperty] private int level;

        public ObservableCollection<TypeSimplifiedViewModel> DoubleDamageTo { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> NoDamageFrom { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> HalfDamageFrom { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> HalfDamageTo { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> NoDamageTo { get; set; }
        public ObservableCollection<TypeSimplifiedViewModel> DoubleDamageFrom { get; set; }

        private PokemonType pType;

        public TypeExtendedViewModel(PokemonType source)
        {
            pType = source;
            name = "-";
            icon = string.Format(Properties.Resources.TypeIconPath, source.TypeId);
            largeIcon = string.Format(Properties.Resources.TypeLargeIconPath, source.TypeId);

            level = 19;
            DoubleDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();
            foreach (string item in pType.DoubleDamageTo)
            {
                DoubleDamageTo.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level + DoubleDamageTo.Count * 4 - DoubleDamageTo.Count;
            if (DoubleDamageTo.Count == 0)
            {
                DoubleDamageTo.Add(new TypeSimplifiedViewModel());
            }

            NoDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();
            foreach (string item in pType.NoDamageFrom)
            {
                NoDamageFrom.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level + NoDamageFrom.Count * 4 - NoDamageFrom.Count;
            if (NoDamageFrom.Count == 0)
            {
                NoDamageFrom.Add(new TypeSimplifiedViewModel());
            }

            HalfDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();
            foreach (string item in pType.HalfDamageFrom)
            {
                HalfDamageFrom.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level + HalfDamageFrom.Count * 2 - HalfDamageFrom.Count;
            if (HalfDamageFrom.Count == 0)
            {
                HalfDamageFrom.Add(new TypeSimplifiedViewModel());
            }

            HalfDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();
            foreach (string item in pType.HalfDamageTo)
            {
                HalfDamageTo.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level - HalfDamageTo.Count * 2 - HalfDamageTo.Count;
            if (HalfDamageTo.Count == 0)
            {
                HalfDamageTo.Add(new TypeSimplifiedViewModel());
            }

            NoDamageTo = new ObservableCollection<TypeSimplifiedViewModel>();
            foreach (string item in pType.NoDamageTo)
            {
                NoDamageTo.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level - NoDamageTo.Count * 4 - NoDamageTo.Count;
            if (NoDamageTo.Count == 0)
            {
                NoDamageTo.Add(new TypeSimplifiedViewModel());
            }

            DoubleDamageFrom = new ObservableCollection<TypeSimplifiedViewModel>();
            foreach (string item in pType.DoubleDamageFrom)
            {
                DoubleDamageFrom.Add(new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level - DoubleDamageFrom.Count * 4 - DoubleDamageFrom.Count;
            if (DoubleDamageFrom.Count == 0)
            {
                DoubleDamageFrom.Add(new TypeSimplifiedViewModel());
            }

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[pType.KeyName];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[pType.KeyName];
                    break;
            }

            foreach (TypeSimplifiedViewModel item in DoubleDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in NoDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in HalfDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in HalfDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in NoDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeSimplifiedViewModel item in DoubleDamageFrom)
            {
                item.SetLanguage(language);
            }
        }
    }
}
