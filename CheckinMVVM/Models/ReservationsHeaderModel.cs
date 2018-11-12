using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CheckinMVVM.Models
{
    public class ReservationsHeaderModel
    {
        [JsonProperty("ReservationID")]
        public string ReservationID { get; set; }

        [JsonProperty("GuestName")]
        public string GuestName { get; set; }

        [JsonProperty("MainClientName")]
        public string MainClientName { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("RoomNumber")]
        public string RoomNumber { get; set; }

        [JsonProperty("NumberOfVisits")]
        public string NumberOfVisits { get; set; }

        [JsonProperty("TotalNumberOfVisits")]
        public string TotalNumberOfVisits { get; set; }

        [JsonProperty("TextColor")]
        public string TextColor { get; set; }

        [JsonProperty("RoomStatusIndicatorPath")]
        public string RoomIndicatorImgPath { get; set; }

        [JsonProperty("ReservationStatusIndicatorPath")]
        public string ReservationStatusImgPath { get; set; }

    }
}
