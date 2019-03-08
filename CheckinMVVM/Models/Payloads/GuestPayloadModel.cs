using System;
using Newtonsoft.Json;

namespace CheckinMVVM.Models.Payloads
{
    public class GuestPayloadModel
    {
        [JsonProperty("XhotelId")]
        public string HotelCode { get; set; }

        [JsonProperty("XreservaId")]
        public string ReservationId { get; set; }

        [JsonProperty("XtipoDocId")]
        public string IdDocumentType { get; set; }

        [JsonProperty("XnumeroDoc")]
        public string IdPassportNumber { get; set; }

        [JsonProperty("Xorden")]
        public string GuestNumber { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Name1")]
        public string FirstName { get; set; }

        [JsonProperty("Name2")]
        public string LastName { get; set; }

        [JsonProperty("Parge")]
        public string Gender { get; set; }

        [JsonProperty("SmtpAddr")]
        public string Email { get; set; }

        [JsonProperty("MobileNo")]
        public string MobileNo { get; set; }

        [JsonProperty("HouseNum1")]
        public string HouseNumber { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("City1")]
        public string City { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("County")]
        public string Nationality { get; set; }

        [JsonProperty("Langu")]
        public string Language { get; set; }

        [JsonProperty("Gbdat")]
        public string DateOfBirth { get; set; }

        [JsonProperty("XfechaExpiry")]
        public string PassportExpiryDate { get; set; }
    }
}
