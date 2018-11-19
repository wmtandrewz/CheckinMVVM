using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class RoomStatusModel
    {
        [JsonProperty("RoomStatus")]
        public string RoomStatus { get; set; }

        [JsonProperty("CheckinStatus")]
        public string CheckinStatus { get; set; }

        [JsonProperty("ReservationId")]
        public string ReservationId { get; set; }
    }
}
