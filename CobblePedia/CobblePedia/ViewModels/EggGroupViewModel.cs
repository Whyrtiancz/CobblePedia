namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class EggGroupViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string eggIcon;

        private EggGroup eggGroup;

        public EggGroupViewModel()
        {
            name = string.Empty;
            eggIcon = string.Format(Properties.Resources.TypeIconPath, "empty"); ;
        }

        public EggGroupViewModel(EggGroup source)
        {
            eggGroup = source;
            eggIcon = string.Format(Properties.Resources.EggGroupPicture, eggGroup.EggGroupId);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            if (eggGroup == null)
                return;

            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[eggGroup.KeyName];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[eggGroup.KeyName];
                    break;
            }
        }
    }

}

