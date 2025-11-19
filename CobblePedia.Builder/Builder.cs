namespace CobblePedia.Builder
{
    using CobblePedia.Builder.Jar;

    using CobblePedia.Models;
    using CobblePedia.Models.Utils;

    using Newtonsoft.Json;

    public class Builder
    {
        public Dictionary<string, string> FR { get; private set; }
        public Dictionary<string, string> EN { get; private set; }

        private static Builder? builder;

        internal PokeAPI pokeAPI { get; private set; }
        internal Minecraft minecraft { get; private set; }
        internal Cobblemon cobblemon { get; private set; }
        internal Rctmod rctmod { get; private set; }

        public static Builder Instance
        {
            get
            {
                if (null == builder)
                {
                    builder = new Builder();
                }

                return builder;
            }
        }

        private Builder()
        {
            this.FR = new Dictionary<string, string>();
            this.EN = new Dictionary<string, string>();
        }

        public void BuildData(string minecraftJar, string cobblemonJar, string rtcmodJar)
        {
            LoadCustomL10N();
            pokeAPI = new PokeAPI();
            minecraft = new Minecraft(minecraftJar);
            cobblemon = new Cobblemon(cobblemonJar);
            rctmod = new Rctmod(rtcmodJar);
            // Cas particulier des mouvements qui nécessitent le chargement des langues de Cobblemon pour la traduction
            pokeAPI.LoadMoves();
            CompleteMissingL10N();
            CheckTrainersTeam();
            CheckMoves();
            Save();

            CobblePedia pedia = CobblePedia.Pedia;
        }

        private void LoadCustomL10N()
        {
            FR.Add("SpriteDefault", "Normal");
            FR.Add("SpriteMale", "Mâle");
            FR.Add("SpriteFemale", "Femelle");
            FR.Add("SpriteDefaultShiny", "Chromatique");
            FR.Add("SpriteMaleShiny", "Mâle Chromatique");
            FR.Add("SpriteFemaletShiny", "Femelle Chromatique");

            EN.Add("SpriteDefault", "Normal");
            EN.Add("SpriteMale", "Male");
            EN.Add("SpriteFemale", "Female");
            EN.Add("SpriteDefaultShiny", "Shiny");
            EN.Add("SpriteMaleShiny", "Male Shiny");
            EN.Add("SpriteFemaletShiny", "Female Shiny");

            FR.Add("Evolution_Method_level_up", "Par niveau (>={0})");
            FR.Add("Evolution_Method_friendship", "Par amitié (>={0})");
            FR.Add("Evolution_Method_battle_critical_hits", "Par attaques critiques (>={0})");
            FR.Add("Evolution_Method_item_interact", "Par objet ({0})");
            FR.Add("Evolution_Method_trade", "Par échange ({0})");

            EN.Add("Evolution_Method_level_up", "By level (>={0})");
            EN.Add("Evolution_Method_friendship", "By friendship (>={0})");
            EN.Add("Evolution_Method_battle_critical_hits", "By critical hits (>={0})");
            EN.Add("Evolution_Method_item_interact", "By item ({0})");
            EN.Add("Evolution_Method_trade", "By trade ({0})");

            FR.Add("Trainer_Type_", "");
            FR.Add("Trainer_Type_leader", "Leader");
            FR.Add("Trainer_Type_battleground", "Champ de bataille");
            FR.Add("Trainer_Type_team_rocket", "Team Rocket");
            FR.Add("Trainer_Type_team_shadow", "Team Shadow");
            FR.Add("Trainer_Type_champ", "Champion");
            FR.Add("Trainer_Type_normal", "Normal");
            FR.Add("Trainer_Type_rival", "Rival");
            FR.Add("Trainer_Type_team_galactic", "Team Galactic");
            FR.Add("Trainer_Type_e4", "Conseil des 4");
            FR.Add("Trainer_Type_ligh_of_ruin", "Ligh of ruin");

            EN.Add("Trainer_Type_", "");
            EN.Add("Trainer_Type_leader", "Leader");
            EN.Add("Trainer_Type_battleground", "Battleground");
            EN.Add("Trainer_Type_team_rocket", "Team Rocket");
            EN.Add("Trainer_Type_team_shadow", "Team Shadow");
            EN.Add("Trainer_Type_champ", "Champion");
            EN.Add("Trainer_Type_normal", "Normal");
            EN.Add("Trainer_Type_rival", "Rival");
            EN.Add("Trainer_Type_team_galactic", "Team Galactic");
            EN.Add("Trainer_Type_e4", "Conseil des 4");
            EN.Add("Trainer_Type_ligh_of_ruin", "Ligh of ruin");

            FR.Add("Trainer_Series_", "");
            FR.Add("Trainer_Series_unbound", "Illimité");
            FR.Add("Trainer_Series_radicalred", "Radical Rouge");
            FR.Add("Trainer_Series_bdsp", "BDSP");

            EN.Add("Trainer_Series_", "");
            EN.Add("Trainer_Series_unbound", "Unbound");
            EN.Add("Trainer_Series_radicalred", "Radical Red");
            EN.Add("Trainer_Series_bdsp", "BDSP");

            FR.Add("Moves_Egg", "Eclosion");
            FR.Add("Moves_Level", "Niveau {0}");
            FR.Add("Moves_TM", "CT/TM");
            FR.Add("Moves_Tutor", "Tuteur");

            EN.Add("Moves_Egg", "Egg");
            EN.Add("Moves_Level", "Level {0}");
            EN.Add("Moves_TM", "CT/CM");
            EN.Add("Moves_Tutor", "Tutor");
        }

        private void CheckTrainersTeam()
        {
            Console.WriteLine("Check Trainer Team invalid links");
            foreach (Trainer trainer in rctmod.Trainers.Values)
            {
                foreach (TrainerTeam team in trainer.Teams)
                {
                    if (!cobblemon.Pokemon.ContainsKey(team.SpeciesId))
                    {
                        Console.WriteLine("  -> {0} / {1}", trainer.TrainerId, team.SpeciesId);
                    }
                }
            }
        }

        private void CheckMissingMoves(string speciesId, List<PokemonMove> moves)
        {
            List<PokemonMove> toRemove = new List<PokemonMove>();

            foreach (PokemonMove move in moves)
            {
                if (!pokeAPI.Moves.ContainsKey(move.MoveId))
                {
                    Console.WriteLine("  -> {0} / {1}", speciesId, move.MoveId);
                    toRemove.Add(move);
                }
            }
            foreach (PokemonMove move in toRemove)
            {
                moves.Remove(move);
            }
        }

        private void CheckMoves()
        {
            Console.WriteLine("Check missing moves");
            foreach (Pokemon pokemon in cobblemon.Pokemon.Values)
            {
                CheckMissingMoves(pokemon.SpeciesId, pokemon.EggMoves);
                CheckMissingMoves(pokemon.SpeciesId, pokemon.TMMoves);
                CheckMissingMoves(pokemon.SpeciesId, pokemon.LeveledMoves);
                CheckMissingMoves(pokemon.SpeciesId, pokemon.TutorMoves);
                CheckMissingMoves(pokemon.SpeciesId, pokemon.FormChangeMoves);
            }
        }

        private void SearchMissingL10N(string key, string id)
        {
            if (id.StartsWith("cobblemon:"))
            {
                if (!FR.ContainsKey(id))
                {
                    FR.Add(id, JsonHelper.GetTranslation(cobblemon.FR, key));
                    EN.Add(id, JsonHelper.GetTranslation(cobblemon.EN, key));
                }
            }
            if (id.StartsWith("minecraft:"))
            {
                if (!FR.ContainsKey(id))
                {
                    if (EN.ContainsKey(key))
                    {
                        FR.Add(id, JsonHelper.GetTranslation(minecraft.FR, key));
                        EN.Add(id, JsonHelper.GetTranslation(minecraft.EN, key));
                    }
                    else
                    {
                        key = string.Format("block.{0}", id.Replace(':', '.'));
                        if (EN.ContainsKey(key))
                        {
                            FR.Add(id, JsonHelper.GetTranslation(minecraft.FR, key));
                            EN.Add(id, JsonHelper.GetTranslation(minecraft.EN, key));
                        }
                        else
                        {
                            FR.Add(id, id);
                            EN.Add(id, id);
                        }
                    }
                }
            }
        }
        private void CompleteMissingL10N()
        {
            foreach (Pokemon pokemon in cobblemon.Pokemon.Values)
            {
                foreach (Drop drop in pokemon.Drops)
                {
                    string key = string.Format("item.{0}", drop.DropId.Replace(':', '.'));
                    SearchMissingL10N(key, drop.DropId);
                }

                foreach (Evolution item in pokemon.Evolutions)
                {
                    if (item.ItemKey == null)
                    {
                        continue;
                    }
                    string key = string.Format("item.{0}", item.ItemKey.Replace(':', '.'));
                    SearchMissingL10N(key, item.ItemKey);
                }
            }

            foreach (Trainer trainer in rctmod.Trainers.Values)
            {
                string key;

                foreach (Bag item in trainer.Bags)
                {
                    key = string.Format("item.{0}", item.BagId.Replace(':', '.'));
                    SearchMissingL10N(key, item.BagId);
                }

                foreach (TrainerTeam item in trainer.Teams)
                {
                    if (item.HeldItem == null)
                        continue;

                    key = string.Format("item.{0}", item.HeldItem.Replace(':', '.'));
                    SearchMissingL10N(key, item.HeldItem);
                }

                if (null == trainer.SignatureItem)
                    continue;

                key = string.Format("item.{0}", trainer.SignatureItem.Replace(':', '.'));
                SearchMissingL10N(key, trainer.SignatureItem);
            }

            Console.WriteLine("Missing french translation");
            foreach (var item in FR)
            {
                if (item.Value == null || item.Value.Length <= 2)
                {
                    if (EN[item.Key] != null)
                    {
                        //Console.WriteLine("  - " + item.Key);
                        FR[item.Key] = EN[item.Key];
                    }
                }
            }
        }

        private void Save()
        {
            string jsonContent = JsonConvert.SerializeObject(this.FR, Formatting.Indented);
            jsonContent = jsonContent.Replace("\\n\\n", "\\n");
            string filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "fr.json");
            File.WriteAllText(filePath, jsonContent);

            jsonContent = JsonConvert.SerializeObject(this.EN, Formatting.Indented);
            jsonContent = jsonContent.Replace("\\n\\n", "\\n");
            filePath = Path.Combine(ConfigurationHelper.CobblePediaDataPath, "en.json");
            File.WriteAllText(filePath, jsonContent);

            pokeAPI.Save();
            cobblemon.Save();
            rctmod.Save();
        }
    }
}
