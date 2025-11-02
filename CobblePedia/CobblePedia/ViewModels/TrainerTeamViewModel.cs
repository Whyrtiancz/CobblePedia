namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class TrainerTeamViewModel : ObservableObject
    {
        [ObservableProperty] private SpeciesViewModel species;

        [ObservableProperty] private MoveViewModel move1;
        [ObservableProperty] private MoveViewModel move2;
        [ObservableProperty] private MoveViewModel move3;
        [ObservableProperty] private MoveViewModel move4;

        private readonly TrainerTeam team;

        private TrainerTeamViewModel() { }

        internal TrainerTeamViewModel(TrainerTeam source)
        {
            team = source;

            species = new SpeciesViewModel(CobblePedia.Pedia.Pokemon[team.SpeciesId]);
            if (team.MoveSet.Count > 0)
            {
                move1 = new MoveViewModel(CobblePedia.Pedia.Moves[team.MoveSet[0]]);
                if (team.MoveSet.Count > 1)
                {
                    move2 = new MoveViewModel(CobblePedia.Pedia.Moves[team.MoveSet[1]]);
                    if (team.MoveSet.Count > 2)
                    {
                        move3 = new MoveViewModel(CobblePedia.Pedia.Moves[team.MoveSet[2]]);
                        if (team.MoveSet.Count > 3)
                        {
                            move4 = new MoveViewModel(CobblePedia.Pedia.Moves[team.MoveSet[3]]);
                        }
                    }
                }
            }
        }
    }
}
