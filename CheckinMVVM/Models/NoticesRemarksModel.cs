using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class NoticesRemarksModel
    {
        [JsonProperty("ReservationId")]
        public string ReservationId { get; set; }

        [JsonProperty("HotelCode")]
        public string HotelCode { get; set; }

        [JsonProperty("RemarksList")]
        public List<RemarksModel> RemarksList { get; set; }

        [JsonProperty("NoticesList")]
        public List<NoticesModel> NoticesList { get; set; }
    }
}
