using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Models;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class RegistrationCardViewModel
    {

        public ICommand PageOnLoadCommand { get;}

        private ReservationDetailsModel _reservationDetailsModel = Constants.SelectedReservationDetailSet;
        public ReservationDetailsModel ReservationDetails
        {
            get
            {
                return _reservationDetailsModel;
            }
        }

        private GuestDetailsModel _guestDetailsModel = Constants.SelectedReservationDetailSet.GuestProfilesList.FirstOrDefault(x=>x.GuestNumber == 1);
        public GuestDetailsModel MainGuestProfile
        {
            get
            {
                if (!string.IsNullOrEmpty(_guestDetailsModel.DateOfBirth))
                {
                    _guestDetailsModel.DateOfBirth = Convert.ToDateTime(_guestDetailsModel.DateOfBirth).ToString("dd MMM yyyy");
                }
                return _guestDetailsModel;
            }
        }

        private ObservableCollection<GuestDetailsModel> _guestList = new ObservableCollection<GuestDetailsModel>(Constants.SelectedReservationDetailSet.GuestProfilesList);
        public ObservableCollection<GuestDetailsModel> GuestList
        {
            get
            {
                return _guestList;
            }
        }

        public RegistrationCardViewModel()
        {
            PageOnLoadCommand = new Command(PangeOnLoad);
        }

        private async void PangeOnLoad()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
