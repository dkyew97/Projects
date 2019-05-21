using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APUPayStore
{
    class ShopInventory
    {
        [JsonProperty("shop_id")]
        public string shop { get; set; }
        [JsonProperty("item_name")]
        public string item { get; set; }
        [JsonProperty("item_price")]
        public decimal price { get; set; }
    }
}
