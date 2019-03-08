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
using CheckinMVVM.Views;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class RemarksViewModel : INotifyPropertyChanged
    {
        public ICommand PageOnLoadCommand { get; }
        public ICommand RemarkTappedCommand { get; }
        public ICommand AddNewRemarkCommand { get; }

        private INavigation Navigation { get; set; }

        public ReservationDetailsModel ReservationDetails { get; } = Constants.SelectedReservationDetailSet;

        private ObservableCollection<RemarksModel> _remarksListSource = new ObservableCollection<RemarksModel>();
        public ObservableCollection<RemarksModel> RemarksListSource
        {
            get
            {
                return _remarksListSource;
            }
            set
            {
                _remarksListSource = value;
                OnPropertyChanged("RemarksListSource");
            }
        }

        private RemarksModel _selectedRemark;
        public RemarksModel SelectedRemark
        {
            get
            {
                return _selectedRemark;
            }

            set
            {
                Constants.SelectedRemark = value;
                _selectedRemark = value;
                OnPropertyChanged("SelectedRemark");
            }
        }

        private bool _isVisibleIndicator = true;
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

        private bool _isRunningIndicator = true;
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

        private bool _isVisiblePage = false;
        public bool IsVisiblePage
        {
            get
            {
                return _isVisiblePage;
            }

            set
            {
                _isVisiblePage = value;
                OnPropertyChanged("IsVisiblePage");
            }
        }

        public RemarksViewModel(INavigation _navigation)
        {
            this.Navigation = _navigation;

            PageOnLoadCommand = new Command( PageOnLoad);
            RemarkTappedCommand = new Command(async () => await RemarkItemTapped());
            AddNewRemarkCommand = new Command(async () => await AddNewRemark());

        }

        private async Task AddNewRemark()
        {
            await Navigation.PushAsync(new EditRemarkView("new"));
        }

        private async Task RemarkItemTapped()
        {
            if (SelectedRemark.RemarkCategory!="Main Remark")
            {
                await Navigation.PushAsync(new EditRemarkView("modify"));
            }
            else
            {
                PageOnLoad();
            }
        }

        private void PageOnLoad()
        {
            Constants.SelectedRemark = null;

            List<RemarksModel> remarks = new List<RemarksModel>();

            var mainRemark = Constants.SelectedReservationDetailSet.MainRemark;
            if (!string.IsNullOrEmpty(mainRemark))
            {
                remarks.Add(new RemarksModel
                {
                    RemarkCategory = "Main Remark",
                    RemarkDescription = mainRemark
                });
            }

            foreach (var item in Constants.SelectedNoticesRemarksSet.RemarksList)
            {
                remarks.Add(new RemarksModel
                {
                    RemarkCategory = item.RemarkCategory,
                    RemarkDescription = item.RemarkDescription
                });
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                RemarksListSource = new ObservableCollection<RemarksModel>(remarks);
                IsVisibleIndicator = false;
                IsRunningIndicator = false;
                IsVisiblePage = true;
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
