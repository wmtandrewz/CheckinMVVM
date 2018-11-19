using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Helpers;
using CheckinMVVM.Models;
using CheckinMVVM.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class RoomListViewModel :INotifyPropertyChanged
    {
        public INavigation NavigationStack { get; private set; }
        public ICommand PageOnLoadCommand { get; }

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

        public RoomListViewModel(INavigation navigation)
        {
            this.NavigationStack = navigation;
            PageOnLoadCommand = new Command(async () => await PageOnLoad());
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
