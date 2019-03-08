using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class NoticesModel
    {
        [JsonProperty("NoticeCategory")]
        public string NoticeCategory { get; set; }

        [JsonProperty("NoticeDescription")]
        public string NoticeDescription { get; set; }
    }
}
