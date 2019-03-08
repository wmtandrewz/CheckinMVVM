using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models.Payloads
{
    public class RoomPayloadModel
    {
        [JsonProperty("ImHotelId")]
        public string HotelCode { get; set; }

        [JsonProperty("ImReservaId")]
        public string ReservationID { get; set; }

        [JsonProperty("ImHabitacionId")]
        public string RoomNumber { get; set; }
    }
}
