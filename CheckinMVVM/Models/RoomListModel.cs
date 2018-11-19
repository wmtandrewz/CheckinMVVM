using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CheckinMVVM.Models
{
    public class RoomListModel
    {
        [JsonProperty("RoomNumber")]
        public string RoomNumber { get; set; }

        [JsonProperty("RoomType")]
        public string RoomType { get; set; }

        [JsonProperty("Status")]
        public string RoomStatus { get; set; }

        [JsonProperty("StatusImage")]
        public string RoomStatusImage { get; set; }

        [JsonProperty("StatusColor")]
        public string RoomStatusColor { get; set; }

        [JsonProperty("RoomPreferences")]
        public string RoomPreferences { get; set; }
    }
}

