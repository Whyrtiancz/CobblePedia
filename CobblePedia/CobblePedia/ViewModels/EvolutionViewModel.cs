namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class EvolutionViewModel : ObservableObject
    {
        [ObservableProperty] private PokemonSimplifiedViewModel evolveTo;

        private Evolution evolution;

        public EvolutionViewModel(Evolution source)
        {
            evolution = source;

            evolveTo = new PokemonSimplifiedViewModel(CobblePedia.Pedia.Pokemon[evolution.EvolveTo]);
        }
    }
}
