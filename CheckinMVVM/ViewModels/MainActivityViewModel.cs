﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Helpers;
using CheckinMVVM.Models;
using CheckinMVVM.Services;
using CheckinMVVM.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class MainActivityViewModel : INotifyPropertyChanged
    {
        public INavigation NavigationStack { get; private set; }
        public ICommand PageOnLoadCommand { get; }
        public ICommand LoadRoomListCommand { get; }
        public ICommand GuestViewCommand { get; }

        private ReservationsHeaderModel _reservationsHeaderModel;
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

                if(_reservationsHeaderModel != null && _reservationsHeaderModel.RoomNumber.Contains("N/A") || string.IsNullOrEmpty(_reservationsHeaderModel.RoomNumber))
                {
                    BadgeRoomIcon = "Icons/warning.png";
                }
                else if (_reservationsHeaderModel != null && _reservationsHeaderModel.RoomNumber.All(Char.IsDigit))
                {
                    BadgeRoomIcon = "Icons/tick.png";
                }
                else
                {
                    BadgeRoomIcon = string.Empty;
                }

                if (_reservationsHeaderModel != null && _reservationsHeaderModel.Status.Contains("Expected"))
                {
                    BadgeCheckinIcon = "Icons/warning.png";
                }
                else if (_reservationsHeaderModel != null && _reservationsHeaderModel.Status.Contains("Checked-In"))
                {
                    BadgeCheckinIcon = "Icons/tick.png";
                }
                else
                {
                    BadgeCheckinIcon = string.Empty;
                }

            }
        }

        private ReservationDetailsModel _reservationDetailsModel;
        public ReservationDetailsModel ReservationDetails
        {
            get
            {
                return _reservationDetailsModel;
            }
            set
            {
                _reservationDetailsModel = value;
                Constants.SelectedReservationDetailSet = value;
                OnPropertyChanged("ReservationDetails");
            }
        }

        private bool _isRunningIndicator = false;
        public bool IsRunningIndicator
        {
            get
            {
                return _isRunningIndicator;
            }
            set
            {
                _isRunningIndicator = value;
                OnPropertyChanged("IsRunningIndicator");
            }
        }

        private bool _isVisibleIndicator = false;
        public bool IsVisibleIndicator
        {
            get
            {
                return _isVisibleIndicator;
            }
            set
            {
                _isVisibleIndicator = value;
                OnPropertyChanged("IsVisibleIndicator");
            }
        }

        private bool _isVisibleData = false;
        public bool IsVisibleData
        {
            get
            {
                return _isVisibleData;
            }
            set
            {
                _isVisibleData = value;
                OnPropertyChanged("IsVisibleData");
            }
        }

        private string _badgeRoomIcon = string.Empty;
        public string BadgeRoomIcon
        {
            get 
            {
                return _badgeRoomIcon;
            }

            set
            {
                _badgeRoomIcon = value;
                OnPropertyChanged("BadgeRoomIcon");
            }
        }

        private string _badgeCheckinIcon = string.Empty;
        public string BadgeCheckinIcon
        {
            get
            {
                return _badgeCheckinIcon;
            }

            set
            {
                _badgeCheckinIcon = value;
                OnPropertyChanged("BadgeCheckinIcon");
            }
        }

        public MainActivityViewModel(ReservationsHeaderModel reservationsHeader,INavigation navigation)
        {
            ReservationsHeader = reservationsHeader;
            Constants.SelectedReservationHeader = reservationsHeader;

            this.NavigationStack = navigation;

            PageOnLoadCommand = new Command(async () => await PageOnLoad());
            LoadRoomListCommand = new Command(async () => await LoadRoomListPage());
            GuestViewCommand = new Command(async () => await LoadGuestView());
        }

        private async Task LoadGuestView()
        {
            await this.NavigationStack.PushAsync(new GuestView());
        }

        private async Task LoadRoomListPage()
        {
            await this.NavigationStack.PushAsync(new RoomListView(NavigationStack));
        }

        private async Task PageOnLoad()
        {
            IsRunningIndicator = true;
            IsVisibleIndicator = true;
            IsVisibleData = false;

            RoomStatusModel roomStatusModel = new RoomStatusModel();

            var responce = await GetAPIservice.GetReservationDetails(Settings.HotelCode, ReservationsHeader.ReservationID);

            if(!string.IsNullOrEmpty(responce))
            {
                ReservationDetailsModel reservationDetails = JsonConvert.DeserializeObject<ReservationDetailsModel>(responce);
                if(reservationDetails != null)
                {
                    reservationDetails.RoomStatusColor = "Black";

                    if (!string.IsNullOrEmpty(ReservationsHeader.RoomNumber) && ReservationsHeader.RoomNumber.All(Char.IsDigit))
                    {

                        var roomResponce = await GetAPIservice.GetRoomStatus(Settings.HotelCode, ReservationsHeader.RoomNumber);
                        if (!string.IsNullOrEmpty(roomResponce))
                        {
                            roomStatusModel = JsonConvert.DeserializeObject<RoomStatusModel>(roomResponce);
                            if (roomStatusModel != null)
                            {
                                reservationDetails.RoomStatusColor = roomStatusModel.RoomStatus == "Clean" ? "Green" : roomStatusModel.RoomStatus == "Inspected" ? "Orange" : "Red";
                                reservationDetails.RoomStatus = !string.IsNullOrEmpty(roomStatusModel.RoomStatus) ? roomStatusModel.RoomStatus : "Not Assigned";
                            }

                        }
                    }
                    else
                    {
                        reservationDetails.RoomStatusColor = "Red";
                        reservationDetails.RoomStatus = !string.IsNullOrEmpty(roomStatusModel.RoomStatus) ? roomStatusModel.RoomStatus : "Not Assigned";
                        reservationDetails.RoomNumber = reservationDetails.RoomNumber.All(Char.IsDigit) && !string.IsNullOrEmpty(reservationDetails.RoomNumber) ? reservationDetails.RoomNumber : "Not Assigned";
                    }

                    ReservationDetails = reservationDetails;
                    IsRunningIndicator = false;
                    IsVisibleIndicator = false;
                    IsVisibleData = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

