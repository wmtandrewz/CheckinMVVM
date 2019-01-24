using System;
namespace CheckinMVVM.Models
{
    public class GuestCheckinModel
    {
        public string ReservationId { get; set; }

        public string HotelCode { get; set; }

        public string GuestNumber { get; set; }

        public string SignatureBase64 { get; set; }

        public string IsCheckedIn { get; set; }

        public string CheckinMethod { get; set; }
    }
}
