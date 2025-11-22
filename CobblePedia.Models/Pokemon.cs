namespace CobblePedia.Models
{
    public class Pokemon
    {
        //private List<string> _types;

        //public List<string> GetTypes()
        //{
        //    return _types; 
        //}

        //public void PopulateTypesList()
        //{
        //    _types = new List<string>();
        //    _types.Add(PrimaryType);
        //    if (SecondaryType != null)
        //    {
        //        _types.Add(SecondaryType);
        //    }
        //}

        public static string FileName { get; } = "pokemon.json";

        public bool Implemented { get; set; }
        public bool IsForm { get; set; }
        public int NationalPokedexNumber { get; set; }
        public string SpeciesId { get; set; }

        #region General
        public string Generation { get; set; }
        public List<string> Types { get; set; }
        //public string PrimaryType { get; set; }
        //public string SecondaryType { get; set; }
        #endregion General

        #region Physique
        public int Height { get; set; }
        public int Weight { get; set; }
        #endregion Physique

        #region Reproduction
        public float MaleRatio { get; set; }
        public int EggCycles { get; set; }
        public List<string> EggGroups { get; set; }
        #endregion Reproduction

        #region Evolution
        public string PreEvolutionSpeciesId { get; set; }
        public List<Evolution> Evolutions { get; set; }
        #endregion Evolution

        #region Statistiques
        public int BaseHP { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefence { get; set; }
        public int BaseSpecialAttack { get; set; }
        public int BaseSpecialDefence { get; set; }
        public int BaseSpeed { get; set; }
        public int BaseTotal { get; set; }

        public int EvHP { get; set; }
        public int EvAttack { get; set; }
        public int EvDefence { get; set; }
        public int EvSpecialAttack { get; set; }
        public int EvSpecialDefence { get; set; }
        public int EvSpeed { get; set; }
        #endregion Statistiques

        public int BaseExperienceYield { get; set; }
        public string BaseExperienceGroup { get; set; }
        public int BaseFriendship { get; set; }
        public int CatchRate { get; set; }
        public bool IsDynamaxBlocked { get; set; }

        public List<PokemonAbility> Abilities { get; set; }
        public List<PokemonMove> LeveledMoves { get; set; }
        public List<PokemonMove> EggMoves { get; set; }
        public List<PokemonMove> TMMoves { get; set; }
        public List<PokemonMove> TutorMoves { get; set; }
        public List<PokemonMove> FormChangeMoves { get; set; }
        public List<Drop> Drops { get; set; }
        public List<Spawn> Spawns { get; set; }
        public List<string> Forms { get; set; }

        public string KeyName
        {
            get
            {
                return string.Format(Properties.Resources.SpeciesKeyName, SpeciesId);
            }
        }
        public string KeyDescription
        {
            get
            {
                return string.Format(Properties.Resources.SpeciesKeyDescription, SpeciesId);
            }
        }

        public Sprite Picture { get; set; }

        public Pokemon() { }

        public Pokemon(Species species)
        {
            Initialize(species);
            Evolutions = species.Evolutions;

            // Forms
            Forms = new List<string>();
            foreach (Species item in species.Forms)
            {
                Forms.Add(item.SpeciesId);
            }

            IsForm = false;
        }

        public Pokemon(Species species, Species form, int formIndex)
        {
            Initialize(species);

            Implemented = form.Implemented;
            SpeciesId = form.SpeciesId;
            NationalPokedexNumber = species.NationalPokedexNumber * 10000 + formIndex;
            PreEvolutionSpeciesId = form.PreEvolutionSpeciesId;
            Generation = form.Generation;
            if (form.PrimaryType != null)
            {
                Types = new List<string>();
                Types.Add(form.PrimaryType);
                if (form.SecondaryType != null)
                {
                    Types.Add(form.SecondaryType);
                }
            }
            Height = form.Height;
            Weight = form.Weight;

            BaseHP = form.BaseHP;
            BaseAttack = form.BaseAttack;
            BaseDefence = form.BaseDefence;
            BaseSpecialAttack = form.BaseSpecialAttack;
            BaseSpecialDefence = form.BaseSpecialDefence;
            BaseSpeed = form.BaseSpeed;
            BaseTotal = form.BaseTotal;

            EvHP = form.EvHP;
            EvAttack = form.EvAttack;
            EvDefence = form.EvDefence;
            EvSpecialAttack = form.BaseSpecialAttack;
            EvSpecialDefence = form.EvSpecialDefence;
            EvSpeed = form.EvSpeed;

            BaseExperienceYield = form.BaseExperienceYield;
            BaseExperienceGroup = form.BaseExperienceGroup;
            BaseFriendship = form.BaseFriendship;
            CatchRate = form.CatchRate;
            IsDynamaxBlocked = form.IsDynamaxBlocked;

            MaleRatio = form.MaleRatio;
            EggCycles = form.EggCycles;

            Abilities = form.Abilities;
            EggGroups = form.EggGroups;
            LeveledMoves = form.LeveledMoves;
            EggMoves = form.EggMoves;
            TMMoves = form.TMMoves;
            TutorMoves = form.TutorMoves;
            FormChangeMoves = form.FormChangeMoves;
            Drops = form.Drops;
            Picture = form.Picture;
            Spawns = form.Spawns;

            Evolutions = form.Evolutions;

            // Forms
            Forms = new List<string>();
            foreach (Species item in form.Forms)
            {
                Forms.Add(item.SpeciesId);
            }

            IsForm = true;
        }

        private void Initialize(Species species)
        {
            Implemented = species.Implemented;
            SpeciesId = species.SpeciesId;
            NationalPokedexNumber = species.NationalPokedexNumber;
            PreEvolutionSpeciesId = species.PreEvolutionSpeciesId;
            Generation = species.Generation;
            Types = new List<string>();
            Types.Add(species.PrimaryType);
            if (species.SecondaryType != null)
            {
                Types.Add(species.SecondaryType);
            }
            Height = species.Height;
            Weight = species.Weight;

            BaseHP = species.BaseHP;
            BaseAttack = species.BaseAttack;
            BaseDefence = species.BaseDefence;
            BaseSpecialAttack = species.BaseSpecialAttack;
            BaseSpecialDefence = species.BaseSpecialDefence;
            BaseSpeed = species.BaseSpeed;
            BaseTotal = species.BaseTotal;

            EvHP = species.EvHP;
            EvAttack = species.EvAttack;
            EvDefence = species.EvDefence;
            EvSpecialAttack = species.BaseSpecialAttack;
            EvSpecialDefence = species.EvSpecialDefence;
            EvSpeed = species.EvSpeed;

            BaseExperienceYield = species.BaseExperienceYield;
            BaseExperienceGroup = species.BaseExperienceGroup;
            BaseFriendship = species.BaseFriendship;
            CatchRate = species.CatchRate;
            IsDynamaxBlocked = species.IsDynamaxBlocked;

            MaleRatio = species.MaleRatio;
            EggCycles = species.EggCycles;

            Abilities = species.Abilities;
            EggGroups = species.EggGroups;
            LeveledMoves = species.LeveledMoves;
            EggMoves = species.EggMoves;
            TMMoves = species.TMMoves;
            TutorMoves = species.TutorMoves;
            FormChangeMoves = species.FormChangeMoves;
            Drops = species.Drops;
            Picture = species.Picture;
            Spawns = species.Spawns;
        }

        public List<Evolution> GetEvolutionTo(string evolveTo)
        {
            return Evolutions.Where(item => item.EvolveTo == evolveTo).ToList();
        }
    }
}