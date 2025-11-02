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

        public ObservableCollection<TypeViewModel> DoubleDamageTo { get; set; }
        public ObservableCollection<TypeViewModel> NoDamageFrom { get; set; }
        public ObservableCollection<TypeViewModel> HalfDamageFrom { get; set; }
        public ObservableCollection<TypeViewModel> HalfDamageTo { get; set; }
        public ObservableCollection<TypeViewModel> NoDamageTo { get; set; }
        public ObservableCollection<TypeViewModel> DoubleDamageFrom { get; set; }

        private PokemonType pType;

        public TypeExtendedViewModel(PokemonType source)
        {
            pType = source;
            name = "-";
            icon = string.Format(Properties.Resources.TypeIconPath, source.TypeId);
            largeIcon = string.Format(Properties.Resources.TypeLargeIconPath, source.TypeId);

            level = 19;
            DoubleDamageTo = new ObservableCollection<TypeViewModel>();
            foreach (string item in pType.DoubleDamageTo)
            {
                DoubleDamageTo.Add(new TypeViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level + DoubleDamageTo.Count * 4 - DoubleDamageTo.Count;
            if (DoubleDamageTo.Count == 0)
            {
                DoubleDamageTo.Add(new TypeViewModel());
            }

            NoDamageFrom = new ObservableCollection<TypeViewModel>();
            foreach (string item in pType.NoDamageFrom)
            {
                NoDamageFrom.Add(new TypeViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level + NoDamageFrom.Count * 4 - NoDamageFrom.Count;
            if (NoDamageFrom.Count == 0)
            {
                NoDamageFrom.Add(new TypeViewModel());
            }

            HalfDamageFrom = new ObservableCollection<TypeViewModel>();
            foreach (string item in pType.HalfDamageFrom)
            {
                HalfDamageFrom.Add(new TypeViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level + HalfDamageFrom.Count * 2 - HalfDamageFrom.Count;
            if (HalfDamageFrom.Count == 0)
            {
                HalfDamageFrom.Add(new TypeViewModel());
            }

            HalfDamageTo = new ObservableCollection<TypeViewModel>();
            foreach (string item in pType.HalfDamageTo)
            {
                HalfDamageTo.Add(new TypeViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level - HalfDamageTo.Count * 2 - HalfDamageTo.Count;
            if (HalfDamageTo.Count == 0)
            {
                HalfDamageTo.Add(new TypeViewModel());
            }

            NoDamageTo = new ObservableCollection<TypeViewModel>();
            foreach (string item in pType.NoDamageTo)
            {
                NoDamageTo.Add(new TypeViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level - NoDamageTo.Count * 4 - NoDamageTo.Count;
            if (NoDamageTo.Count == 0)
            {
                NoDamageTo.Add(new TypeViewModel());
            }

            DoubleDamageFrom = new ObservableCollection<TypeViewModel>();
            foreach (string item in pType.DoubleDamageFrom)
            {
                DoubleDamageFrom.Add(new TypeViewModel(CobblePedia.Pedia.Types[item]));
            }
            level = level - DoubleDamageFrom.Count * 4 - DoubleDamageFrom.Count;
            if (DoubleDamageFrom.Count == 0)
            {
                DoubleDamageFrom.Add(new TypeViewModel());
            }


            SetLanguage("en");
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

            foreach (TypeViewModel item in DoubleDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeViewModel item in NoDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeViewModel item in HalfDamageFrom)
            {
                item.SetLanguage(language);
            }
            foreach (TypeViewModel item in HalfDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeViewModel item in NoDamageTo)
            {
                item.SetLanguage(language);
            }
            foreach (TypeViewModel item in DoubleDamageFrom)
            {
                item.SetLanguage(language);
            }
        }
    }
}
