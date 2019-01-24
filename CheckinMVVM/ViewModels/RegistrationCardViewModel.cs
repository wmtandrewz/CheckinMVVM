using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Models;
using CheckinMVVM.Views;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class RegistrationCardViewModel :INotifyPropertyChanged
    {

        public ICommand PageOnLoadCommand { get;}
        public ICommand SignatureTappedCommand { get; }

        private INavigation Navigation;

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

        private GuestSignatureModel _selectedSignature;
        public GuestSignatureModel SelectedSignature
        {
            get 
            {
                return _selectedSignature;
            }

            set
            {
                _selectedSignature = value;
                OnPropertyChanged("SelectedSignature");
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

        private ObservableCollection<GuestSignatureModel> _signatureList;
        public ObservableCollection<GuestSignatureModel> SignatureList
        {
            get
            {
                return _signatureList;
            }
            set
            {
                _signatureList.Clear();
                _signatureList = value;
                OnPropertyChanged("SignatureList");
            }
        }

        public int SignatureListLength { get; } = (Constants.SelectedReservationDetailSet.GuestProfilesList.Count * 200) + 50;

        public RegistrationCardViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            PageOnLoadCommand = new Command(PangeOnLoad);
            SignatureTappedCommand = new Command(async () => await SignatureTapped());

            //Initialize Signatures
            #region Initialize Signatures
            List<GuestSignatureModel> guestSignautesModelList = new List<GuestSignatureModel>();
            if (GuestList != null) {
                foreach (var item in Constants.SelectedReservationDetailSet.GuestProfilesList)
                {
                    guestSignautesModelList.Add(new GuestSignatureModel
                    {
                        GuestNumber = item.GuestNumber,
                        GuestName = $"{item.GuestFirstName} {item.GuestLastName}",
                        GuestNameColor = "#6D2276",//purple
                        GuestSignatureImage = ImageSource.FromFile("Icons/SignatureImage.jpg"),
                        IsSignatureAdded = false,
                        CellColor = "White"
                    });
                }
            }
            _signatureList = new ObservableCollection<GuestSignatureModel>(guestSignautesModelList);
            #endregion Initialize Signatures
        }

        private async Task SignatureTapped()
        {
            if (SelectedSignature!=null)
            {
                Constants.SelectedGuestSignature = SelectedSignature;
                await Navigation.PushAsync(new SignaturePadView());
            }
        }

        private void PangeOnLoad()
        {
            SelectedSignature = null;

            List<GuestSignatureModel> signaturesBindingList = new List<GuestSignatureModel>();

            if (Constants.SignaturesList.Count > 0)
            {

                signaturesBindingList.AddRange(Constants.SignaturesList);

                var signatureNotAvailableList = Constants.SelectedReservationDetailSet.GuestProfilesList.Where(x => !Constants.SignaturesList.Any(s=>s.GuestNumber == x.GuestNumber)).ToList();

                foreach (var item in signatureNotAvailableList)
                {
                    signaturesBindingList.Add(new GuestSignatureModel
                    {
                        GuestNumber = item.GuestNumber,
                        GuestName = $"{item.GuestFirstName} {item.GuestLastName}",
                        GuestNameColor = "#6D2276",//purple
                        GuestSignatureImage = ImageSource.FromFile("Icons/SignatureImage.jpg"),
                        IsSignatureAdded = false,
                        CellColor = "White"
                    });
                }

                SignatureList = new ObservableCollection<GuestSignatureModel>(signaturesBindingList);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
