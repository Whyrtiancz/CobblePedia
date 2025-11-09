namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class EvolutionViewModel : ObservableObject
    {
        [ObservableProperty] private PokemonSimplifiedViewModel evolveTo;
        [ObservableProperty] private string method;

        private string methodKey;
        private Evolution evolution;

        public EvolutionViewModel()
        {
            method = "";
        }

        public EvolutionViewModel(Evolution source)
        {
            evolution = source;
            methodKey = string.Format("Evolution_Method_{0}", evolution.Method);

            evolveTo = new PokemonSimplifiedViewModel(CobblePedia.Pedia.Pokemon[evolution.EvolveTo]);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            if (evolution == null)
                return;

            Dictionary<string, string> translation = new Dictionary<string, string>();

            switch (language)
            {
                case "fr":
                    translation = CobblePedia.Pedia.FR;
                    break;
                default:
                    translation = CobblePedia.Pedia.EN;
                    break;
            }

            switch (evolution.Method)
            {
                case "level_up":
                    Method = string.Format(translation[methodKey], evolution.MinLevel);
                    break;
                case "friendship":
                    Method = string.Format(translation[methodKey], evolution.MinFriendship);
                    break;
                case "battle_critical_hits":
                    Method = string.Format(translation[methodKey], evolution.MinBattleCriticalHits);
                    break;
                case "item_interact":
                    Method = string.Format(translation[methodKey], translation[evolution.ItemKey]);
                    break;
                case "trade":
                    Method = string.Format(translation[methodKey], "");
                    break;
            }
        }
    }
}