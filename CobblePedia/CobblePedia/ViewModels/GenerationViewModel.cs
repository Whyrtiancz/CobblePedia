namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class GenerationViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string region;

        private Generation generation;

        public GenerationViewModel(Generation source)
        {
            generation = source;
            region = generation.Region;
            name = "-";
            SetLanguage("en");
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[generation.KeyName];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[generation.KeyName];
                    break;
            }
        }
    }
}
