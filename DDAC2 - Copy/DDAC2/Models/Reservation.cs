using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC2.Models
{
    public class Reservation
    {
        [Key]
        [JsonProperty(PropertyName ="id")]
        public string ReservationID { get; set; }

        [JsonProperty(PropertyName = "buyer_name")]
        public string BuyerName { get; set; }

        [JsonProperty(PropertyName = "buyer_email")]
        public string BuyerEmail { get; set; }

        [JsonProperty(PropertyName = "buyer_phone")]
        public string BuyerPhone { get; set; }

        [JsonProperty(PropertyName = "vin_number")]
        public string CarVinNumber { get; set; }

        [JsonProperty(PropertyName = "reservation_status")]
        public string Status { get; set; }
    }
}
