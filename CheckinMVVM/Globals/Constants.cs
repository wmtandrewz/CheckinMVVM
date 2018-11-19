using System;
using CheckinMVVM.Models;

namespace CheckinMVVM.Globals
{
    public static class Constants
    {
        public static string AccessToken { get; set; }
        public static string TokenExpiresIn { get; set; }


        public static ReservationsHeaderModel SelectedReservationHeader { get; set; }
    }
}
