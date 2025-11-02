namespace CobblePedia.Models
{
    using System;

    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Pokemon
    {
        public static string FileName { get; } = "species.json";

        public bool Implemented { get; set; }
        public int NationalPokedexNumber { get; set; }
        public string SpeciesId { get; set; }
        public string PreEvolutionSpeciesId { get; set; }

        public string Generation { get; set; }
        public string PrimaryType { get; set; }
        public string SecondaryType { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

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

        public int BaseExperienceYield { get; set; }
        public string BaseExperienceGroup { get; set; }
        public int BaseFriendship { get; set; }
        public int CatchRate { get; set; }
        public bool IsDynamaxBlocked { get; set; }

        public float MaleRatio { get; set; }
        public int EggCycles { get; set; }

        public List<PokemonAbility> Abilities { get; set; }
        public List<Evolution> Evolutions { get; set; }
        public List<string> EggGroups { get; set; }
        public List<PokemonMove> LeveledMoves { get; set; }
        public List<PokemonMove> EggMoves { get; set; }
        public List<PokemonMove> TMMoves { get; set; }
        public List<PokemonMove> TutorMoves { get; set; }
        public List<PokemonMove> FormChangeMoves { get; set; }
        public List<Drop> Drops { get; set; }
        public List<Spawn> Spawns { get; set; }
        public List<Pokemon> Forms { get; set; }


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

        internal Pokemon() { }

        internal Pokemon(JObject source, Pokemon parent = null)
        {
            Implemented = JsonHelper.GetBoolValue(source, "implemented");
            NationalPokedexNumber = JsonHelper.GetIntValue(source, "nationalPokedexNumber");
            SpeciesId = NamingHelper.RebuildPokemonName(JsonHelper.GetStringValue(source, "name"));
            if (JsonHelper.GetStringValue(source, "preEvolution") != null)
            {
                string preEvolution = NamingHelper.RebuildPokemonName(JsonHelper.GetStringValue(source, "preEvolution"));
                switch (preEvolution)
                {
                    case "nidoranf":
                        PreEvolutionSpeciesId = "nidoran-f";
                        break;
                    case "nidoranm":
                        PreEvolutionSpeciesId = "nidoran-m";
                        break;
                    case "mimejr":
                        PreEvolutionSpeciesId = "mime-jr";
                        break;
                    case "typenull":
                        PreEvolutionSpeciesId = "type:-null";
                        break;
                    case "jangmoo":
                        PreEvolutionSpeciesId = "jangmo-o";
                        break;
                    case "hakamoo":
                        PreEvolutionSpeciesId = "hakamo-o";
                        break;
                    case "sinistea-tea_authenticity=phony":
                        PreEvolutionSpeciesId = "sinistea";
                        break;
                    case "linoone-galarian":
                        preEvolution = "zigzagoon-galar";
                        break;
                    case "meowth-galarian":
                        preEvolution = "meowth-galar";
                        break;
                    case "corsola-galarian":
                        preEvolution = "corsola-galar";
                        break;
                    case "farfetchd-galarian":
                        preEvolution = "farfetchd-galar";
                        break;
                    case "mrmime-galarian":
                        preEvolution = " mr-mime-galar";
                        break;
                    case "yamask-galarian":
                        preEvolution = "yamask-galar";
                        break;
                    case "basculin-fish_stripes=white":
                        preEvolution = "basculin-white-striped";
                        break;
                    case "sneasel-hisuian":
                        preEvolution = "sneasel-hisui";
                        break;
                    case "qwilfish-hisuian":
                        preEvolution = "qwilfish-hisui";
                        break;
                    case "wooper-paldean":
                        preEvolution = "wooper-paldea";
                        break;
                    case "poltchageist-matcha_authenticity=counterfeit":
                        preEvolution = "poltchageist";
                        break;
                    default:
                        PreEvolutionSpeciesId = preEvolution;
                        break;
                }
            }

            if (null != parent)
            {
                SpeciesId = string.Format("{0}-{1}", parent.SpeciesId, SpeciesId);
            }

            string translate = (string)source.SelectToken("pokedex[0]");
            if (null != translate)
            {
                CobblePedia.Pedia.FR.Add(KeyDescription, JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, translate));
                CobblePedia.Pedia.FR.Add(KeyName, JsonHelper.GetTranslation(CobblePedia.Pedia.translationFR, translate.Replace(".desc", ".name")));
                CobblePedia.Pedia.EN.Add(KeyDescription, JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, translate));
                CobblePedia.Pedia.EN.Add(KeyName, JsonHelper.GetTranslation(CobblePedia.Pedia.translationEN, translate.Replace(".desc", ".name")));
            }

            Generation = JsonHelper.GetStringValue(source, "labels", "labels[0]").Substring(0, 4);
            PrimaryType = JsonHelper.GetStringValue(source, "primaryType");
            SecondaryType = JsonHelper.GetStringValue(source, "secondaryType");
            Height = JsonHelper.GetIntValue(source, "height");
            Weight = JsonHelper.GetIntValue(source, "weight");

            BaseHP = JsonHelper.GetIntValue(source, "baseStats", "baseStats.hp");
            BaseAttack = JsonHelper.GetIntValue(source, "baseStats", "baseStats.attack");
            BaseDefence = JsonHelper.GetIntValue(source, "baseStats", "baseStats.defence");
            BaseSpecialAttack = JsonHelper.GetIntValue(source, "baseStats", "baseStats.special_attack");
            BaseSpecialDefence = JsonHelper.GetIntValue(source, "baseStats", "baseStats.special_defence");
            BaseSpeed = JsonHelper.GetIntValue(source, "baseStats", "baseStats.speed");
            BaseTotal = BaseHP + BaseAttack + BaseDefence + BaseSpecialAttack + BaseSpecialDefence + BaseSpeed;

            EvHP = JsonHelper.GetIntValue(source, "evYield", "evYield.hp");
            EvAttack = JsonHelper.GetIntValue(source, "evYield", "evYield.attack");
            EvDefence = JsonHelper.GetIntValue(source, "evYield", "evYield.defence");
            EvSpecialAttack = JsonHelper.GetIntValue(source, "evYield", "evYield.special_attack");
            EvSpecialDefence = JsonHelper.GetIntValue(source, "evYield", "evYield.special_defence");
            EvSpeed = JsonHelper.GetIntValue(source, "evYield", "evYield.speed");

            BaseExperienceYield = JsonHelper.GetIntValue(source, "baseExperienceYield");
            BaseExperienceGroup = JsonHelper.GetStringValue(source, "experienceGroup");
            BaseFriendship = JsonHelper.GetIntValue(source, "baseFriendship");
            CatchRate = JsonHelper.GetIntValue(source, "catchRate");
            IsDynamaxBlocked = JsonHelper.GetBoolValue(source, "dynamaxBlocked");

            MaleRatio = JsonHelper.GetFloatValue(source, "maleRatio");
            EggCycles = JsonHelper.GetIntValue(source, "eggCycles");

            Abilities = new List<PokemonAbility>();
            foreach (string item in source.SelectTokens("abilities").Values<string>())
            {
                Abilities.Add(ReadAbility(item));
            }

            Evolutions = new List<Evolution>();
            foreach (JToken token in source.SelectTokens("evolutions"))
            {
                foreach (JToken item in token.Children())
                {
                    Evolutions.Add(ReadEvolution(item));
                }
            }

            EggGroups = new List<string>(source.SelectTokens("eggGroups").Values<string>());

            LeveledMoves = new List<PokemonMove>();
            EggMoves = new List<PokemonMove>();
            TMMoves = new List<PokemonMove>();
            TutorMoves = new List<PokemonMove>();
            FormChangeMoves = new List<PokemonMove>();
            foreach (string moveDescription in source.SelectTokens("moves").Values<string>())
            {
                string[] desc = moveDescription.Split(':');
                PokemonMove move = new PokemonMove();
                move.MoveId = desc[1];

                switch (desc[0])
                {
                    case "egg":
                        EggMoves.Add(move);
                        break;
                    case "tm":
                        TMMoves.Add(move);
                        break;
                    case "tutor":
                        TutorMoves.Add(move);
                        break;
                    case "form_change":
                        FormChangeMoves.Add(move);
                        break;
                    default:
                        move.Level = int.Parse(desc[0]);
                        LeveledMoves.Add(move);
                        break;
                }
            }

            Drops = new List<Drop>();
            foreach (JToken token in source.SelectTokens("drops.entries"))
            {
                foreach (JToken item in token.Children())
                {
                    Drops.Add(ReadDrop(item));
                }
            }

            Spawns = CobblePedia.Pedia.Spawns.Where(item => item.NationalPokedexNumber == this.NationalPokedexNumber).ToList();

            string spriteId = SpeciesId;
            switch (SpeciesId)
            {
                case "exeggcute-alola-bias":
                    spriteId = "exeggutor-alola";
                    break;
                case "flabebe":
                    spriteId = "flabebe-red";
                    break;
                case "floette":
                    spriteId = "floette-red";
                    break;
                case "florges":
                    spriteId = "florges-red";
                    break;
                case "gastrodon":
                    spriteId = "gastrodon-west";
                    break;
                case "mr. mime":
                    spriteId = "mr-mime";
                    break;
                case "mr. mime-galar":
                    spriteId = "mr-mime-galar";
                    break;
                case "pikachu-alola-bias":
                    spriteId = "pikachu-alola-cap";
                    break;
                case "pichu-alola-bias":
                    spriteId = "pichu-spiky-eared";
                    break;
                case "pikachu-partner":
                    spriteId = "pikachu-partner-cap";
                    break;
                case "tauros-paldea-aqua":
                case "tauros-paldea-blaze":
                case "tauros-paldea-combat":
                    spriteId = spriteId + "-breed";
                    break;
            }

            if (CobblePedia.Pedia.Sprites.ContainsKey(spriteId))
            {
                Picture = CobblePedia.Pedia.Sprites[spriteId];
            }
            else
            {
                Console.WriteLine("  - {0}", this.SpeciesId);
                Picture = CobblePedia.Pedia.Sprites["none"];
            }

            Forms = new List<Pokemon>();
            if (source.ContainsKey("forms"))
            {
                foreach (JObject item in source.SelectToken("forms"))
                {
                    Pokemon form = new Pokemon(item, this);
                    Forms.Add(form);
                }
            }

        }

        private PokemonAbility ReadAbility(string description)
        {
            PokemonAbility ability = new PokemonAbility();
            ability.AbilityId = description;
            ability.IsHidden = description.StartsWith("h:");
            if (ability.IsHidden)
            {
                ability.AbilityId = description.Substring(2);
            }

            return ability;
        }

        private Evolution ReadEvolution(JToken token)
        {
            Evolution evolution = new Evolution();

            evolution.Method = token.Value<string>("variant");
            evolution.EvolveTo = NamingHelper.RebuildPokemonName(token.Value<string>("result"));
            evolution.IsConsumeHeldItem = token.Value<bool>("consumeHeldItem");
            evolution.LearnableMoves = new List<string>(token.SelectTokens("learnableMoves").Values<string>());

            return evolution;
        }

        private Drop ReadDrop(JToken token)
        {
            Drop drop = new Drop();

            drop.DropId = token.Value<string>("item");
            if (null != token.SelectToken("percentage"))
            {
                drop.Percentage = (float)token.SelectToken("percentage");
            }
            if (null != token.SelectToken("quantityRange"))
            {
                drop.QuantityRange = (string)token.SelectToken("quantityRange");
            }

            return drop;
        }
    }
}