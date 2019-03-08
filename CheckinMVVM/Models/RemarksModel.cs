using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class RemarksModel
    {
        [JsonProperty("RemarkCategory")]
        public string RemarkCategory { get; set; }

        [JsonProperty("RemarkDescription")]
        public string RemarkDescription { get; set; }
    }
}
