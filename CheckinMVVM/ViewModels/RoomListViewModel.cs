using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Helpers;
using CheckinMVVM.Models;
using CheckinMVVM.Models.Payloads;
using CheckinMVVM.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class RoomListViewModel :INotifyPropertyChanged
    {
        public INavigation NavigationStack { get; private set; }
        public ICommand PageOnLoadCommand { get; }
        public ICommand RoomSelectedCommand { get; }

        private ObservableCollection<RoomListModel> _roomList;
        public ObservableCollection<RoomListModel> RoomingList
        {
            get
            {
                return _roomList;
            }
            set
            {
                _roomList = value;
                OnPropertyChanged("RoomingList");
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

        private RoomListModel _selectedRoom = null;
        public RoomListModel SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }

        public RoomListViewModel(INavigation navigation)
        {
            this.NavigationStack = navigation;
            PageOnLoadCommand = new Command(async () => await PageOnLoad());
            RoomSelectedCommand = new Command(async () => await RoomSelected());
        }

        private async Task RoomSelected()
        {
            IsListVisible = false;
            IsIndicatorVisible = true;

            RoomPayloadModel roomPayload = new RoomPayloadModel()
            {
                HotelCode = Settings.HotelCode,
                ReservationID = Constants.SelectedReservationHeader.ReservationID,
                RoomNumber = SelectedRoom.RoomNumber
            };

            var responce = await PostAPIservice.AssignRoom(roomPayload);

            if(responce == "success")
            {
                Constants.SelectedReservationHeader.RoomNumber = roomPayload.RoomNumber;
                Constants.SelectedReservationHeader.RoomIndicatorImgPath = SelectedRoom.RoomStatus == "CLEAN" ? "Icons/CleanRoom.png" : SelectedRoom.RoomStatus == "INSPECTED" ? "Icons/InspectedRoom.png" : "Icons/DirtyRoom.png";

                Constants.SelectedReservationDetailSet.RoomNumber = roomPayload.RoomNumber;
                Constants.SelectedReservationDetailSet.RoomStatus = SelectedRoom.RoomStatus;
                Constants.SelectedReservationDetailSet.RoomStatusColor = SelectedRoom.RoomStatus == "CLEAN" ? "Green" : SelectedRoom.RoomStatus == "INSPECTED" ? "Orange" : "Red";

                MessagingCenter.Send<RoomListViewModel>(this, "RoomDetailsChanged");

                await Application.Current.MainPage.DisplayAlert("Success!", "Room has been assigned successfully.", "OK");
                await NavigationStack.PopAsync();
            }

            IsListVisible = true;
            IsIndicatorVisible = false;
        }

        private async Task PageOnLoad()
        {
            IsIndicatorVisible = true;
            try
            {

                var result = await GetAPIservice.GetRoomDetails(Settings.HotelCode, Constants.SelectedReservationHeader.ReservationID,"X");

                var list = JsonConvert.DeserializeObject<List<RoomListModel>>(result);

                Device.BeginInvokeOnMainThread(() =>
                {
                    RoomingList = new ObservableCollection<RoomListModel>(list);
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
