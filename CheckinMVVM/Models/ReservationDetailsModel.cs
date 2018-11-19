using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class ReservationDetailsModel
    {
        [JsonProperty("ReservationId")]
        public string ReservationId { get; set; }

        [JsonProperty("ClientName")]
        public string ClientName { get; set; }

        [JsonProperty("ArrivalDate")]
        public string ArrivalDate { get; set; }

        [JsonProperty("DepartureDate")]
        public string DepartureDate { get; set; }

        [JsonProperty("NumberOfNights")]
        public string NumberOfNights { get; set; }

        [JsonProperty("NumberOfPax")]
        public string NumberOfPax { get; set; }

        [JsonProperty("RoomType")]
        public string RoomType { get; set; }

        [JsonProperty("UpgradedRoomType")]
        public string UpgradedRoomType { get; set; }

        [JsonProperty("RoomNumber")]
        public string RoomNumber { get; set; }

        [JsonProperty("RoomStatus")]
        public string RoomStatus { get; set; }

        [JsonProperty("RoomStatusColor")]
        public string RoomStatusColor { get; set; }

        [JsonProperty("MealPlan")]
        public string MealPlan { get; set; }

        [JsonProperty("ArrivalFlight")]
        public string ArrivalFlight { get; set; }

        [JsonProperty("DepartureFlight")]
        public string DepartureFlight { get; set; }
    }
}
