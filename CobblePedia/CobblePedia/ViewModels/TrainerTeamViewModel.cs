namespace CobblePedia.ViewModels
{
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class TrainerTeamViewModel : ObservableObject
    {
        [ObservableProperty] private SearchableObjectViewModel pokemon;
        [ObservableProperty] private int level;
        [ObservableProperty] private string nature;
        [ObservableProperty] private string ability;
        [ObservableProperty] private string gender;

        [ObservableProperty] private MoveViewModel move1;
        [ObservableProperty] private MoveViewModel move2;
        [ObservableProperty] private MoveViewModel move3;
        [ObservableProperty] private MoveViewModel move4;

        public ObservableCollection<SearchableObjectViewModel> Types { get; private set; }

        private TrainerTeamViewModel() { }

        internal TrainerTeamViewModel(TrainerTeam source)
        {
            level = source.Level;
            nature = source.Nature;
            ability = source.Ability;
            gender = source.Gender;

            pokemon = CobblePediaViewModel.Model.GetSearchableObjectViewModel(source.SpeciesId, "@pokemon");

            Pokemon detail = CobblePediaModel.Pedia.Pokemons[source.SpeciesId];
            Types = new ObservableCollection<SearchableObjectViewModel>();
            foreach (string item in detail.Types)
            {
                Types.Add( CobblePediaViewModel.Model.GetSearchableObjectViewModel(item, "@type"));
            }

            if (source.MoveSet.Count > 0)
            {
                move1 = new MoveViewModel(CobblePediaModel.Pedia.Moves[source.MoveSet[0]], MoveViewModel.EMoveLearnType.None);
                if (source.MoveSet.Count > 1)
                {
                    move2 = new MoveViewModel(CobblePediaModel.Pedia.Moves[source.MoveSet[1]], MoveViewModel.EMoveLearnType.None);
                    if (source.MoveSet.Count > 2)
                    {
                        move3 = new MoveViewModel(CobblePediaModel.Pedia.Moves[source.MoveSet[2]], MoveViewModel.EMoveLearnType.None);
                        if (source.MoveSet.Count > 3)
                        {
                            move4 = new MoveViewModel(CobblePediaModel.Pedia.Moves[source.MoveSet[3]], MoveViewModel.EMoveLearnType.None);
                        }
                    }
                }
            }
        }
    }
}
