namespace CobblePedia.Models
{
    using Newtonsoft.Json.Linq;

    public class Bag
    {
        public string BagId { get; set; }
        public int Quantity { get; set; }

        internal Bag() { }

        internal Bag(JObject source)
        {
            BagId = source.SelectToken("item").Value<string>();
            Quantity = source.SelectToken("quantity").Value<int>();
        }
    }
}
