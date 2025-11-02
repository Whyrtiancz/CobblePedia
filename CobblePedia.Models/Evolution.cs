namespace CobblePedia.Models
{
    using System.Collections.Generic;

    public class Evolution
    {
        public string Method { get; set; }
        public string EvolveTo { get; set; }
        public bool IsConsumeHeldItem { get; set; }
        public List<string> LearnableMoves { get; set; }

        /* Requirements */
    }
}