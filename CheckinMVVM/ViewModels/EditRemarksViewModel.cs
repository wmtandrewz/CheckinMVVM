using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Models;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class EditRemarksViewModel : INotifyPropertyChanged
    {
        public ICommand PageOnLoadCommand { get; }
        public ICommand SaveButtonCommand { get; }

        private INavigation Navigation { get; set; }
        private string OperationType { get; set; }


        private string _remarkCategoryText = string.Empty;
        public string RemarkCategoryText
        {
            get
            {
                if (_remarkCategoryText != null)
                {
                    return _remarkCategoryText;
                }
                return Constants.SelectedReservationHeader.ReservationID;
            }

            set
            {
                _remarkCategoryText = value;
                OnPropertyChanged("RemarkCategoryText");
            }
        }

        private string _remarkDescriptionText = string.Empty;
        public string RemarkDescriptionText
        {
            get
            {
                if (_remarkDescriptionText!=null)
                {
                    return _remarkDescriptionText;
                }
                return string.Empty;
            }

            set
            {
                _remarkDescriptionText = value;
                OnPropertyChanged("RemarkDescriptionText");
            }
        }

        private string _title = "Modify Remark";
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                OnPropertyChanged("Title");
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
        public EditRemarksViewModel(INavigation _navigation, string _operationType)
        {
            this.Navigation = _navigation;
            this.OperationType = _operationType;

            Title = _operationType == "modify" ? "Modify Remark" : "Add New Remark";

            PageOnLoadCommand = new Command(PageOnLoad);
            SaveButtonCommand = new Command(async () => await SaveModifiedRemark());
        }

        private async Task SaveModifiedRemark()
        {
            await Navigation.PopAsync();
        }

        private void PageOnLoad()
        {
            RemarkCategoryText = Constants.SelectedRemark != null ? Constants.SelectedRemark.RemarkCategory : Constants.SelectedReservationHeader.ReservationID;
            RemarkDescriptionText = Constants.SelectedRemark != null ? Constants.SelectedRemark.RemarkDescription : string.Empty;

            IsRunningIndicator = false;
            IsVisibleIndicator = false;
            IsVisiblePage = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
