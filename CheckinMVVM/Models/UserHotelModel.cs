using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class UserHotelModel
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("ChBname")]
        public string ADuserName { get; set; }

        [JsonProperty("ExXhotelId")]
        public string HotelCode { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
