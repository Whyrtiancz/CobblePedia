namespace CobblePedia.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface ISearchableObject
    {
        public string ObjectId { get; }
        public string KeyType { get; }
        public object Source { get; }
        public string ViewType { get; }

        public void SetLanguage();

        public bool IsElligible(string searchPattern);
    }
}