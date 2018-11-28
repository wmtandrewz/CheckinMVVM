using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Models;
using CheckinMVVM.Views;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class GuestViewModel : INotifyPropertyChanged
    {
        INavigation NavigationStack;

        public ICommand StartScanCommand { get; }
        public ICommand LoadGuestProfilesCommand { get; }
        public ICommand GuestProfileSelectedCommand { get; }

        private ReservationsHeaderModel _reservationsHeaderModel = Constants.SelectedReservationHeader;
        public ReservationsHeaderModel ReservationsHeader
        {
            get
            {
                return _reservationsHeaderModel;
            }

            set
            {
                _reservationsHeaderModel = value;
                OnPropertyChanged("ReservationsHeader");
            }
        }

        private ObservableCollection<GuestDetailsModel> _guestProfiles;
        public ObservableCollection<GuestDetailsModel> GuestProfiles
        {
            get
            {
                return _guestProfiles;
            }
            set
            {
                _guestProfiles = value;
                OnPropertyChanged("GuestProfiles");
            }
        }

        private GuestDetailsModel _selectedGuestProfile;
        public GuestDetailsModel SelectedGuestProfile
        {
            get
            {
                return _selectedGuestProfile;
            }
            set
            {
                _selectedGuestProfile = value;
                OnPropertyChanged("SelectedGuestProfile");
            }
        }


        public GuestViewModel(INavigation navigation)
        {
            NavigationStack = navigation;
            LoadGuestProfilesCommand = new Command(async () => await LoadGuestProfiles());
            GuestProfileSelectedCommand = new Command(async () => await GuestProfileSelected());
        }

        private async Task GuestProfileSelected()
        {
            if (SelectedGuestProfile!=null)
            {
                Constants.SelectedGuestProfile = SelectedGuestProfile;
                await NavigationStack.PushAsync(new GuestProfileView());
            }
        }


        private async Task LoadGuestProfiles()
        {
            try
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    GuestProfiles = new ObservableCollection<GuestDetailsModel>(Constants.SelectedReservationDetailSet.GuestProfilesList);
                });
            }
            catch (Exception)
            {
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
