namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class Species
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
        public List<Species> Forms { get; set; }

        private string spriteId;
        private string? translateKey;

        public string GetSpriteId()
        {
            return spriteId;
        }
        public string GetTranslateKey()
        {
            return translateKey;
        }

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

        internal Species() { }

        public Species(JObject source, Species parent = null)
        {
            Implemented = JsonHelper.GetBoolValue(source, "implemented");
            NationalPokedexNumber = JsonHelper.GetIntValue(source, "nationalPokedexNumber");
            SpeciesId = NamingHelper.RebuildPokemonName(JsonHelper.GetStringValue(source, "name"));
            translateKey = (string?)source.SelectToken("pokedex[0]");
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

            Generation = JsonHelper.GetStringValue(source, "labels", "labels[0]").Substring(0, 4);
            if (Generation == "cobb")
            {
                Generation = parent.Generation;
            }
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
                    Evolution evolution = ReadEvolution(item);
                    if (evolution.EvolveTo.StartsWith("torterra-") ||
                        evolution.EvolveTo.StartsWith("forretress-") ||
                        evolution.EvolveTo.StartsWith("toxtricity-punk_form=") ||
                        evolution.EvolveTo.StartsWith("gholdengo-netherite_coating=") ||
                        evolution.EvolveTo.StartsWith("palafin-dolphin_form=zero"))
                    {
                        continue;
                    }
                    if (evolution.EvolveTo.StartsWith("vivillon-vivillon_wings=") &&
                        !evolution.EvolveTo.StartsWith("vivillon-vivillon_wings=pokeball"))
                    {
                        continue;
                    }
                    Evolutions.Add(evolution);
                }
            }

            EggGroups = new List<string>();
            foreach (string item in source.SelectTokens("eggGroups").Values<string>())
            {
                if (item == "undiscovered")
                {
                    EggGroups.Add("no-eggs");
                }
                else
                {
                    EggGroups.Add(item);
                }
            }

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

            spriteId = SpeciesId;
            switch (SpeciesId)
            {
                case "darmanitan":
                    spriteId = "darmanitan-standard";
                    break;
                case "darmanitan-galar":
                    spriteId = "darmanitan-galar-standard";
                    break;
                case "deerling":
                    spriteId = "deerling-spring";
                    break;
                case "genesect-water":
                    spriteId = "genesect-douse";
                    break;
                case "genesect-electric":
                    spriteId = "genesect-shock";
                    break;
                case "genesect-fire":
                    spriteId = "genesect-burn";
                    break;
                case "genesect-ice":
                    spriteId = "genesect-chill";
                    break;
                case "keldeo":
                    spriteId = "keldeo-ordinary";
                    break;
                case "landorus":
                    spriteId = "landorus-incarnate";
                    break;
                case "meloetta":
                    spriteId = "meloetta-aria";
                    break;
                case "sawsbuck":
                    spriteId = "sawsbuck-spring";
                    break;
                case "thundurus":
                    spriteId = "thundurus-incarnate";
                    break;
                case "tornadus":
                    spriteId = "tornadus-incarnate";
                    break;
                case "aegislash":
                    spriteId = "aegislash-shield";
                    break;
                case "furfrou":
                    spriteId = "furfrou-natural";
                    break;
                case "gourgeist":
                    spriteId = "gourgeist-average";
                    break;
                case "greninja-bond":
                    spriteId = "greninja-battle-bond";
                    break;
                case "meowstic":
                    spriteId = "meowstic-male";
                    break;
                case "meowstic-f":
                    spriteId = "meowstic-female";
                    break;
                case "pumpkaboo":
                    spriteId = "pumpkaboo-average";
                    break;
                case "scatterbug":
                    spriteId = "scatterbug-icy-snow";
                    break;
                case "spewpa":
                    spriteId = "spewpa-icy-snow";
                    break;
                case "vivillon":
                    spriteId = "vivillon-meadow";
                    break;
                case "vivillon-pokeball":
                    spriteId = "vivillon-poke-ball";
                    break;
                case "xerneas":
                    spriteId = "xerneas-neutral";
                    break;
                case "zygarde":
                    spriteId = "zygarde-50";
                    break;
                case "zygarde-10%":
                    spriteId = "zygarde-10";
                    break;
                case "zygarde-10%-c":
                    spriteId = "zygarde-10-power-construct";
                    break;
                case "zygarde-50%-c":
                    spriteId = "zygarde-50-power-construct";
                    break;
                case "lycanroc":
                    spriteId = "lycanroc-midday";
                    break;
                case "mimikyu":
                    spriteId = "mimikyu-disguised";
                    break;
                case "minior":
                    spriteId = "minior-orange-meteor";
                    break;
                case "minior-meteor":
                    spriteId = "minior-red-meteor";
                    break;
                case "necrozma-dusk-mane":
                    spriteId = "necrozma-dusk";
                    break;
                case "necrozma-dawn-wings":
                    spriteId = "necrozma-dawn";
                    break;
                case "oricorio":
                    spriteId = "oricorio-baile";
                    break;
                case "rockruff-dusk":
                    spriteId = "rockruff-own-tempo";
                    break;
                case "silvally":
                    spriteId = "silvally-normal";
                    break;
                case "type:-null":
                    spriteId = "type-null";
                    break;
                case "wishiwashi":
                    spriteId = "wishiwashi-solo";
                    break;
                case "alcremie":
                    spriteId = "alcremie-vanilla-cream-strawberry-sweet";
                    break;
                case "alcremie-ruby-cream":
                    spriteId = "alcremie-ruby-cream-ribbon-sweet";
                    break;
                case "alcremie-matcha-cream":
                    spriteId = "alcremie-matcha-cream-ribbon-sweet";
                    break;
                case "alcremie-mint-cream":
                    spriteId = "alcremie-mint-cream-ribbon-sweet";
                    break;
                case "alcremie-lemon-cream":
                    spriteId = "alcremie-lemon-cream-strawberry-sweet";
                    break;
                case "alcremie-salted-cream":
                    spriteId = "alcremie-salted-cream-ribbon-sweet";
                    break;
                case "alcremie-ruby-swirl":
                    spriteId = "alcremie-ruby-swirl-ribbon-sweet";
                    break;
                case "alcremie-caramel-swirl":
                    spriteId = "alcremie-caramel-swirl-strawberry-sweet";
                    break;
                case "alcremie-rainbow-swirl":
                    spriteId = "alcremie-rainbow-swirl-ribbon-sweet";
                    break;
                case "eiscue":
                    spriteId = "eiscue-ice";
                    break;
                case "eiscue-noice-face":
                    spriteId = "eiscue-noice";
                    break;
                case "indeedee":
                    spriteId = "indeedee-male";
                    break;
                case "indeedee-f":
                    spriteId = "indeedee-female";
                    break;
                case "morpeko":
                    spriteId = "morpeko-full-belly";
                    break;
                case "polteageist":
                    spriteId = "polteageist-phony";
                    break;
                case "sinistea":
                    spriteId = "sinistea-phony";
                    break;
                case "toxtricity":
                    spriteId = "toxtricity-amped";
                    break;
                case "toxtricity-gmax":
                    spriteId = "toxtricity-amped-gmax";
                    break;
                case "urshifu":
                    spriteId = "urshifu-single-strike";
                    break;
                case "urshifu-gmax":
                    spriteId = "urshifu-single-strike-gmax";
                    break;
                case "basculegion":
                    spriteId = "basculegion-male";
                    break;
                case "basculegion-f":
                    spriteId = "basculegion-female";
                    break;
                case "enamorus":
                    spriteId = "enamorus-incarnate";
                    break;
                case "dudunsparce":
                    spriteId = "dudunsparce-two-segment";
                    break;
                case "gimmighoul":
                    spriteId = "gimmighoul-chest";
                    break;
                case "koraidon":
                    spriteId = "koraidon-apex-build";
                    break;
                case "maushold":
                    spriteId = "maushold-family-of-three";
                    break;
                case "maushold-four":
                    spriteId = "maushold-family-of-four";
                    break;
                case "miraidon":
                    spriteId = "miraidon-ultimate-mode";
                    break;
                case "ogerpon-wellspring":
                    spriteId = "ogerpon-wellspring-mask";
                    break;
                case "ogerpon-hearthflame":
                    spriteId = "ogerpon-hearthflame-mask";
                    break;
                case "ogerpon-cornerstone":
                    spriteId = "ogerpon-cornerstone-mask";
                    break;
                case "oinkologne":
                    spriteId = "oinkologne-male";
                    break;
                case "oinkologne-f":
                    spriteId = "oinkologne-female";
                    break;
                case "deoxys":
                    spriteId = "deoxys-normal";
                    break;
                case "arceus":
                    spriteId = "arceus-normal";
                    break;
                case "burmy":
                    spriteId = "burmy-plant";
                    break;
                case "cherrim":
                    spriteId = "cherrim-overcast";
                    break;
                case "palafin":
                    spriteId = "palafin-zero";
                    break;
                case "poltchageist":
                    spriteId = "poltchageist-counterfeit";
                    break;
                case "sinistcha":
                    spriteId = "sinistcha-unremarkable";
                    break;
                case "squawkabilly":
                    spriteId = "squawkabilly-green-plumage";
                    break;
                case "squawkabilly-blue":
                    spriteId = "squawkabilly-blue-plumage";
                    break;
                case "squawkabilly-yellow":
                    spriteId = "squawkabilly-yellow-plumage";
                    break;
                case "squawkabilly-white":
                    spriteId = "squawkabilly-white-plumage";
                    break;
                case "tatsugiri":
                    spriteId = "tatsugiri-curly";
                    break;
                case "wormadam":
                    spriteId = "wormadam-plant";
                    break;
                case "shellos":
                    spriteId = "shellos-west";
                    break;
                case "unown":
                    spriteId = "unown-a";
                    break;
                case "unown-?":
                    spriteId = "unown-question";
                    break;
                case "unown-!":
                    spriteId = "unown-exclamation";
                    break;
                case "exeggcute-alola-bias":
                    spriteId = "exeggute-alola";
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
                case "giratina":
                    spriteId = "giratina-altered";
                    break;
                case "mothim":
                    spriteId = "mothim-plant";
                    break;
                case "shaymin":
                    spriteId = "shaymin-land";
                    break;
                case "basculin":
                    spriteId = "basculin-red-striped";
                    break;
            }

            Forms = new List<Species>();
            if (source.ContainsKey("forms"))
            {
                foreach (JObject item in source.SelectToken("forms"))
                {
                    Species form = new Species(item, this);
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
            Evolution evolution = new Evolution(token);

            //evolution.Method = token.Value<string>("variant");
            //evolution.EvolveTo = NamingHelper.RebuildPokemonName(token.Value<string>("result"));
            //switch(evolution.Method)
            //{
            //    case "level_up":

            //        break;
            //    case "":
            //        break;
            //    case "":
            //        break;
            //}
            //evolution.IsConsumeHeldItem = token.Value<bool>("consumeHeldItem");
            //evolution.LearnableMoves = new List<string>(token.SelectTokens("learnableMoves").Values<string>());

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