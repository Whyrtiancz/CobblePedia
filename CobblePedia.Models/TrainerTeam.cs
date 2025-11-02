namespace CobblePedia.Models
{
    using System.Collections.Generic;

    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class TrainerTeam
    {
        public string SpeciesId { get; set; }
        public string Gender { get; set; }
        public int Level { get; set; }
        public string Nature { get; set; }
        public string Ability { get; set; }

        public List<string> MoveSet { get; set; }
        public string HeldItem { get; set; }

        public int IvHp { get; set; }
        public int IvAttack { get; set; }
        public int IvDefence { get; set; }
        public int IvSpecialAttack { get; set; }
        public int IvSpecialDefence { get; set; }
        public int IvSpeed { get; set; }

        public int EvHp { get; set; }
        public int EvAttack { get; set; }
        public int EvDefence { get; set; }
        public int EvSpecialAttack { get; set; }
        public int EvSpecialDefence { get; set; }
        public int EvSpeed { get; set; }

        internal TrainerTeam() { }

        internal TrainerTeam(JObject source)
        {
            SpeciesId = NamingHelper.RebuildPokemonName(source.SelectToken("species").Value<string>());
            switch (SpeciesId)
            {
                case "kommoo":
                    SpeciesId = "kommo-o";
                    break;
                case "mrmime":
                    SpeciesId = "mr-mime";
                    break;
                case "mrrime":
                    SpeciesId = "mr-rime";
                    break;
                case "mimejr":
                    SpeciesId = "mime-jr";
                    break;
                case "porygonz":
                    SpeciesId = "porygon-z";
                    break;
                case "nidoranf":
                    SpeciesId = "nidoran-f";
                    break;
                case "nidoranm":
                    SpeciesId = "nidoran-m";
                    break;
                case "hooh":
                    SpeciesId = "ho-oh";
                    break;
                case "tapulele":
                    SpeciesId = "tapu-lele";
                    break;
                case "tapufini":
                    SpeciesId = "tapu-fini";
                    break;
                case "tapukoko":
                    SpeciesId = "tapu-koko";
                    break;
                case "tapubulu":
                    SpeciesId = "tapu-bulu";
                    break;
            }
            Gender = source.SelectToken("gender").Value<string>();
            Level = source.SelectToken("level").Value<int>();
            Nature = source.SelectToken("nature").Value<string>();
            Ability = source.SelectToken("ability").Value<string>();

            JToken token = source.SelectToken("ivs");
            if (token != null)
            {
                if (null != token.SelectToken("hp"))
                {
                    IvHp = token.SelectToken("hp").Value<int>();
                }
                if (null != token.SelectToken("att"))
                {
                    IvAttack = token.SelectToken("att").Value<int>();
                }
                if (null != token.SelectToken("def"))
                {
                    IvDefence = token.SelectToken("def").Value<int>();
                }
                if (null != token.SelectToken("spa"))
                {
                    IvSpecialAttack = token.SelectToken("spa").Value<int>();
                }
                if (null != token.SelectToken("spd"))
                {
                    IvSpecialDefence = token.SelectToken("spd").Value<int>();
                }
                if (null != token.SelectToken("spe"))
                {
                    IvSpeed = token.SelectToken("spe").Value<int>();
                }
            }
            token = source.SelectToken("evs");
            if (token != null)
            {
                if (null != token.SelectToken("hp"))
                {
                    EvHp = token.SelectToken("hp").Value<int>();
                }
                if (null != token.SelectToken("att"))
                {
                    EvAttack = token.SelectToken("att").Value<int>();
                }
                if (null != token.SelectToken("def"))
                {
                    EvDefence = token.SelectToken("def").Value<int>();
                }
                if (null != token.SelectToken("spa"))
                {
                    EvSpecialAttack = token.SelectToken("spa").Value<int>();
                }
                if (null != token.SelectToken("spd"))
                {
                    EvSpecialDefence = token.SelectToken("spd").Value<int>();
                }
                if (null != token.SelectToken("spe"))
                {
                    EvSpeed = token.SelectToken("spe").Value<int>();
                }
            }

            MoveSet = new List<string>(source.SelectTokens("moveset").Values<string>());
            if (MoveSet.Contains("visegrip"))
            {
                MoveSet.Clear();
                foreach (string move in source.SelectTokens("moveset").Values<string>())
                {
                    if (move == "visegrip")
                    {
                        MoveSet.Add("vicegrip");
                    }
                    else
                    {
                        MoveSet.Add(move);
                    }
                }
            }
            foreach (string s in MoveSet)
            {
                try
                {
                    Move move = CobblePedia.Pedia.Moves[s];
                }
                catch
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
