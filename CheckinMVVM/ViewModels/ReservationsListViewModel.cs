using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Helpers;
using CheckinMVVM.Models;
using CheckinMVVM.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class ReservationsListViewModel : INotifyPropertyChanged
    {

        public ICommand LoadReservationsListCommand { get; }
        public ICommand ReservationsListOnLoadCommand { get; }
        public ICommand ReservationListItemSelectedCommand { get; }


        private ObservableCollection<ReservationsHeaderModel> _reservationsList;
        public ObservableCollection<ReservationsHeaderModel> ReservationsList
        {
            get
            {
                return _reservationsList;
            }
            set
            {
                _reservationsList = value;
                OnPropertyChanged("ReservationsList");
            }
        }

        private ReservationsHeaderModel _selectedReservation;
        public ReservationsHeaderModel SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged("SelectedReservation");
            }
        }

        private bool _isListVisible = false;
        public bool IsListVisible
        {
            get
            {
                return _isListVisible;
            }
            set
            {
                _isListVisible = value;
                OnPropertyChanged("IsListVisible");
            }
        }

        private bool _isIndicatorVisible = false;
        public bool IsIndicatorVisible
        {
            get
            {
                return _isIndicatorVisible;
            }
            set
            {
                _isIndicatorVisible = value;
                OnPropertyChanged("IsIndicatorVisible");
            }
        }

        public string UserName
        {
            get
            {
                return $"User : {Settings.UserName}";
            }
        }

        private string _hotelDate = DateTime.Now.ToString("dd-MM-yyyy");
        public string HotelDate
        {
            get
            {
                return _hotelDate;
            }
            set
            {
                _hotelDate = value;
                OnPropertyChanged("HotelDate");
            }
        }

        public ReservationsListViewModel()
        {
            ReservationsListOnLoadCommand = new Command(async () => await PageOnLOad());
            LoadReservationsListCommand = new Command(async() => await LoadReservationsList());
            ReservationListItemSelectedCommand = new Command(ReservationListItemSelected);

            MessagingCenter.Subscribe<RoomListViewModel>(this, "RoomDetailsChanged", (sender) =>
            {
                UpdateReservationsList();
            });

        }

        private void ReservationListItemSelected()
        {
            if(SelectedReservation !=null )
            {

                //Get Notices And Remarks
                GetReservationNoticesAndRemarks();

                //Send selected
                MessagingCenter.Send(this, "SelectedReservation", SelectedReservation);

                if (Constants.SignaturesList!=null)
                {
                    Constants.SignaturesList.Clear();
                }

            }
        }

        private async void GetReservationNoticesAndRemarks()
        {
            var responce = await GetAPIservice.GetReservationRemarksNotices(Settings.HotelCode,SelectedReservation.ReservationID);
            if(!string.IsNullOrEmpty(responce))
            {
                NoticesRemarksModel noticesRemarks = JsonConvert.DeserializeObject<NoticesRemarksModel>(responce);
                if (noticesRemarks != null) {
                    Constants.SelectedNoticesRemarksSet = noticesRemarks;
                }
            }
        }

        private async Task PageOnLOad()
        {

            try
            {
                var responce = await GetAPIservice.GetHotelDate(Settings.HotelCode);
                HotelDateModel hotelDateModel = JsonConvert.DeserializeObject<HotelDateModel>(responce);

                if (hotelDateModel != null)
                {
                    HotelDate = DateTime.Parse(hotelDateModel.HotelDate, CultureInfo.CurrentCulture).ToString("dd-MM-yyyy");
                }
            }
            catch(Exception)
            {

            }

            await LoadReservationsList();
        }

        private async Task LoadReservationsList()
        {
            IsIndicatorVisible = true;
            try
            {

                var result = await GetAPIservice.GetReservationsList(Settings.HotelCode, HotelDate);

                var list = JsonConvert.DeserializeObject<List<ReservationsHeaderModel>>(result);

                Device.BeginInvokeOnMainThread(() =>
                {
                    ReservationsList = new ObservableCollection<ReservationsHeaderModel>(list);
                    IsListVisible = true;
                    IsIndicatorVisible = false;
                });
            }
            catch(Exception)
            {
                IsListVisible = false;
                IsIndicatorVisible = false;
            }

        }

        private void UpdateReservationsList()
        {
            IsIndicatorVisible = true;
            IsListVisible = false;

            try
            {

                var tempList = ReservationsList;

                foreach (var item in tempList)
                {
                    if(item.ReservationID == Constants.SelectedReservationHeader.ReservationID)
                    {
                        item.RoomNumber = Constants.SelectedReservationHeader.RoomNumber;
                        item.Status = Constants.SelectedReservationHeader.Status;
                        item.RoomIndicatorImgPath = Constants.SelectedReservationHeader.RoomIndicatorImgPath;
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    ReservationsList = new ObservableCollection<ReservationsHeaderModel>(tempList);
                    IsListVisible = true;
                    IsIndicatorVisible = false;
                });
            }
            catch (Exception)
            {
                IsListVisible = false;
                IsIndicatorVisible = false;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
