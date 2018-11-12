using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class HotelDateModel
    {
        [JsonProperty("HotelID")]
        public string HotelCode { get; set; }

        [JsonProperty("HotelName")]
        public string HotelName { get; set; }

        [JsonProperty("HotelDate")]
        public string HotelDate { get; set; }

        [JsonProperty("AuditHotelDate")]
        public string AuditHotelDate { get; set; }
    }
}
