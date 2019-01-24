using System;
using Xamarin.Forms;

namespace CheckinMVVM.Models
{
    public class GuestSignatureModel
    {
        public int GuestNumber { get; set; }
        public string GuestName { get; set; }
        public string GuestNameColor { get; set; }
        public ImageSource GuestSignatureImage { get; set; }
        public bool IsSignatureAdded { get; set; }
        public string CellColor { get; set; }
    }
}
