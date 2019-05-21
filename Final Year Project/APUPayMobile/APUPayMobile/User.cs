using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace APUPayMobile
{
    class User
    {
        [JsonProperty("user_id")]
        public string UserID { get; set; }
        [JsonProperty("user_password")]
        public string UserPassword { get; set; }
        [JsonProperty("user_email")]
        public string UserEmail { get; set; }
        [JsonProperty("user_type")]
        public string UserType { get; set; }
        [JsonProperty("user_balance")]
        public decimal UserBalance { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }
}