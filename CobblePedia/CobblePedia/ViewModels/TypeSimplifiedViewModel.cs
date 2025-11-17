namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class TypeSimplifiedViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;

        private PokemonType pType;
        private bool isEmpty = true;

        public TypeSimplifiedViewModel()
        {
            name = string.Empty;
            icon = string.Format(Properties.Resources.TypeIconPath, "empty"); ;
        }

        public TypeSimplifiedViewModel(PokemonType source)
        {
            pType = source;
            name = "-";
            icon = string.Format(Properties.Resources.TypeIconPath, source.TypeId);
            isEmpty = false;

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal TypeExtendedViewModel GetExtendedViewModel()
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] = this.pType.TypeId;
            return new TypeExtendedViewModel(this.pType, null, false);
        }

        internal void SetLanguage(string language)
        {
            if (isEmpty)
                return;

            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[pType.KeyName];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[pType.KeyName];
                    break;
            }
        }
    }
}
