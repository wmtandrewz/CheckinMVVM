using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models
{
    public class GuestDetailsModel
    {
        [JsonProperty("GuestNumber")]
        public int GuestNumber { get; set; }

        [JsonProperty("GuestCode")]
        public string GuestCode { get; set; }

        [JsonProperty("IdentificationMethod")]
        public string IdentificationMethod { get; set; }

        [JsonProperty("PassportIdNumber")]
        public string PassportIdNumber { get; set; }

        [JsonProperty("Salutation")]
        public string Salutation { get; set; }

        [JsonProperty("GuestName")]
        public string GuestName { get; set; }

        [JsonProperty("GuestFirstName")]
        public string GuestFirstName { get; set; }

        [JsonProperty("GuestLastName")]
        public string GuestLastName { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("GuestImage")]
        public string GuestImage { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("ContactNumber")]
        public string ContactNumber { get; set; }

        [JsonProperty("HouseNumber")]
        public string HouseNumber { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("CountryKey")]
        public string CountryKey { get; set; }

        [JsonProperty("Nationality")]
        public string Nationality { get; set; }

        [JsonProperty("DateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("DateOfIdExpiry")]
        public string DateOfIdExpiry { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }
    }
}
