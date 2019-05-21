using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APUPayStore
{
    class TransactionObject
    {
        [JsonProperty("transaction_amount")]
        public decimal TransactionAmount { get; set; }
        [JsonProperty("store_id")]
        public string TransactionSeller { get; set; }
        [JsonProperty("transaction_date")]
        public DateTime TransactionTime { get; set; }
        [JsonProperty("transaction_id")]
        public string TransactionID { get; set; }
        [JsonProperty("user_id")]
        public string UserID { get; set; }
    }
}
