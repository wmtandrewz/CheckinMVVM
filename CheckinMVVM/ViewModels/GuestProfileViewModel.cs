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
using CheckinMVVM.Services;
using Microblink.Forms.Core;
using Microblink.Forms.Core.Overlays;
using Microblink.Forms.Core.Recognizers;
using Microblink.Forms.iOS.Recognizers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class GuestProfileViewModel : INotifyPropertyChanged
    {

        public ICommand ScanPassportCommand { get;}
        public ICommand PageOnLoadCommand { get; }
        public ICommand GuestSearchCommand { get; }
        public ICommand IDPickerSelectedChangedCommand { get; }

        private IMicroblinkScanner blinkID;
        private IMrtdRecognizer mrtdRecognizer;
        private ISuccessFrameGrabberRecognizer mrtdSuccessFrameGrabberRecognizer;

        private ObservableCollection<IDMethodModel> _iDPickerItems = new ObservableCollection<IDMethodModel>(Constants.IdentificationMethods);
        public ObservableCollection<IDMethodModel> IDPickerItemlList
        {
            get
            {
                return _iDPickerItems;
            }

            set
            {
                _iDPickerItems = value;
                OnPropertyChanged("IDPickerItemlList");
            }
        }

        private ObservableCollection<GenderModel> _genderItems = new ObservableCollection<GenderModel>(Constants.Genders);
        public ObservableCollection<GenderModel> GenderList
        {
            get
            {
                return _genderItems;
            }

            set
            {
                _genderItems = value;
                OnPropertyChanged("GenderList");
            }
        }

        private ObservableCollection<SalutationModel> _salutationItems = new ObservableCollection<SalutationModel>(Constants.Salutations);
        public ObservableCollection<SalutationModel> SalutationList
        {
            get
            {
                return _salutationItems;
            }

            set
            {
                _salutationItems = value;
                OnPropertyChanged("SalutationList");
            }
        }

        private IDMethodModel _selectedIDMethod;
        public IDMethodModel SelectedIDMethod
        {
            get
            {
                return _selectedIDMethod;
            }

            set
            {
                _selectedIDMethod = value;
                OnPropertyChanged("SelectedIDMethod");
            }
        }

        private GenderModel _selectedGender;
        public GenderModel SelectedGender
        {
            get
            {
                return _selectedGender;
            }

            set
            {
                _selectedGender = value;
                OnPropertyChanged("SelectedGender");
            }
        }

        private SalutationModel _selectedSalutation;
        public SalutationModel SelectedSalutation
        {
            get
            {
                return _selectedSalutation;
            }

            set
            {
                _selectedSalutation = value;
                OnPropertyChanged("SelectedSalutation");
            }
        }

        private GuestDetailsModel _guestProfile;
        public GuestDetailsModel GuestProfile
        {
            get
            {
                return _guestProfile;
            }

            set
            {
                _guestProfile = value;
                OnPropertyChanged("GuestProfile");
            }
        }

        private IMrzResult _scanResult = null;
        public IMrzResult ScanResult
        {
            get
            {
                return _scanResult;
            }

            set
            {
                _scanResult = value;

                OnPropertyChanged("ScanResult");
            }
        }

        private bool _isVisibleHint = true;
        public bool IsVisibleHint
        {
            get
            {
                return _isVisibleHint;
            }

            set
            {
                _isVisibleHint = value;
                OnPropertyChanged("IsVisibleHint");
            }
        }

        private bool _isVisibleGuestInformation = false;
        public bool IsVisibleGuestInformation
        {
            get
            {
                return _isVisibleGuestInformation;
            }

            set
            {
                _isVisibleGuestInformation = value;
                OnPropertyChanged("IsVisibleGuestInformation");
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

        private bool _isVisiblePage = true;
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

        private string _guestNumberText = $"For Guest Number : {Constants.SelectedGuestProfile.GuestNumber}";
        public string GuestNumberText
        {
            get
            {
                return _guestNumberText;
            }

            set
            {
                _guestNumberText = value;
                OnPropertyChanged("GuestNumberText");
            }
        }

        private string _searchBarText;
        public string SearchBarText
        {
            get
            {
                return _searchBarText;
            }

            set
            {
                _searchBarText = value;
                OnPropertyChanged("SearchBarText");
            }
        }

        public GuestProfileViewModel()
        {
            ScanPassportCommand = new Command(ScanPassport);
            PageOnLoadCommand = new Command(PageOnLoad);
            GuestSearchCommand = new Command(async() => await GuestSearchById());
            IDPickerSelectedChangedCommand = new Command(IDSelectedItemChanged);
            InitializeBlinkIdScanner();
        }

        private void IDSelectedItemChanged()
        {

        }

        private async Task GuestSearchById()
        {
            IsVisiblePage = false;
            IsVisibleIndicator = true;
            IsRunningIndicator = true;

            var searchKey = SearchBarText;

            var responce = await GetAPIservice.FindGuestByID(Settings.HotelCode, SelectedIDMethod.Code , searchKey);
            GuestDetailsModel guestDetailsModel = JsonConvert.DeserializeObject<GuestDetailsModel>(responce);

            if(guestDetailsModel != null)
            {
                GuestProfile = guestDetailsModel;

                var GuestProfileTemp = guestDetailsModel;
                GuestProfileTemp.Gender = Constants.Genders.FirstOrDefault(x => x.Code == GuestProfileTemp.Gender).Gender;
                GuestProfileTemp.Salutation = Constants.Salutations.FirstOrDefault(x => x.Code == GuestProfileTemp.Salutation).Salutation;

                SetGuestFormData(GuestProfileTemp);
            }


        }

        private void PageOnLoad()
        {
            IsVisiblePage = false;
            IsVisibleIndicator = true;
            IsRunningIndicator = true;

            if(Constants.SelectedGuestProfile != null)
            {
                if(!string.IsNullOrEmpty(Constants.SelectedGuestProfile.PassportIdNumber))
                {
                    GuestProfile = Constants.SelectedGuestProfile;

                    SetGuestFormData(Constants.SelectedGuestProfile);
                }

                IsVisiblePage = true;
            }
        }

        private void SetGuestFormData(GuestDetailsModel guest)
        {
            //Set ID method
            SelectedIDMethod = new ObservableCollection<IDMethodModel>(Constants.IdentificationMethods).FirstOrDefault(x => x.Code == guest.IdentificationMethod);

            //Set ID
            SearchBarText = Constants.SelectedGuestProfile.PassportIdNumber;

            //Set Gender
            SelectedGender = new ObservableCollection<GenderModel>(Constants.Genders).FirstOrDefault(x => x.Gender == guest.Gender);

            //Set Salutation
            SelectedSalutation = new ObservableCollection<SalutationModel>(Constants.Salutations).FirstOrDefault(x => x.Salutation == guest.Salutation);

            IsVisibleGuestInformation = true;
            IsVisibleHint = false;
            IsVisiblePage = true;

            IsVisibleIndicator = false;
            IsRunningIndicator = false;
        }

        private void ScanPassport()
        {
            IRecognizerCollection recognizerCollection = DependencyService.Get<IRecognizerCollectionFactory>().CreateRecognizerCollection(mrtdSuccessFrameGrabberRecognizer);

            var documentOverlaySettings = DependencyService.Get<IDocumentOverlaySettingsFactory>().CreateDocumentOverlaySettings(recognizerCollection);

            // start scanning
            blinkID.Scan(documentOverlaySettings);
        }

        private void InitializeBlinkIdScanner()
        {
            var microblinkFactory = DependencyService.Get<IMicroblinkScannerFactory>();

            string licenseKey = string.Empty;
            if (Device.RuntimePlatform == Device.iOS)
            {
                licenseKey = "sRwAAAEQY29tLmNobWwuaXQuY2Nmc0iUtoUk/ef1YJ5j3+j5toBM9aJu3EN0SDbB/mPKFx+V0MNFRZwwmNkzl1VU8JhdIYmouNK1poJKQCdOpB6MZ7W1x/pIarGykttt1FFrEHkvAE0NW8NEmidSg8mY+FCvJWVAiQlOHsSXw5YbyjtZv1lGDrU6zai0qefO7+VpwI3w2Q4GwlVybN7SlkSoU1vb8zh0y5Gc2eC5vVDyi7XoSZY/L1lUD1VsBWya5RBti4r7QzPEPy9M/WZp8TNJrw==";
            }
            else
            {
                licenseKey = "sRwAAAAeY29tLm1pY3JvYmxpbmsueGFtYXJpbi5ibGlua2lke7qv4oIhH4ywlU8/Zqc2tpXY3x6Jq4MYktFtYaRbzI3x0Qxrr0NeFEjL6qt8Qcz144x2F5LGg1bHXgkRXbt37saoFv1HJUiJ50Y4YHglH8SqhyfSzsT/C/vQEAKXgZBQuNeeCTY6RttMciEbHHHj7nybEz9+aOoBXfx0+XXx3cU4L6Wbmf/5EAXokQXtEkL3mCR8VC/51ohQVyRIgItohusasIcfe8B+UjUMCZU9v1B0bvHcUKZH6g==";
            }

            blinkID = microblinkFactory.CreateMicroblinkScanner(licenseKey);
            mrtdRecognizer = DependencyService.Get<IMrtdRecognizer>();
            mrtdRecognizer.ReturnFullDocumentImage = true;

            mrtdSuccessFrameGrabberRecognizer = DependencyService.Get<ISuccessFrameGrabberRecognizerFactory>().CreateSuccessFrameGrabberRecognizer(mrtdRecognizer);


            // subscribe to scanning done message
            MessagingCenter.Subscribe<Microblink.Forms.Core.Messages.ScanningDoneMessage>(this, Microblink.Forms.Core.Messages.ScanningDoneMessageId, (sender) =>
            {

                string stringResult = "No valid results.";


                // if user cancelled scanning, sender.ScanningCancelled will be true
                if (sender.ScanningCancelled)
                {

                    stringResult = "Scanning cancelled";
                }
                else
                {

                    // if specific recognizer's result's state is Valid, then it contains data recognized from image
                    if (mrtdRecognizer.Result.ResultState == RecognizerResultState.Valid)
                    {
                        var result = mrtdRecognizer.Result;

                        ScanResult = result.MrzResult;
                        SelectedIDMethod = Constants.IdentificationMethods.FirstOrDefault(x => x.Code == "2");

                        if(result.MrzResult.Gender == "F")
                        {
                            SelectedGender = Constants.Genders.FirstOrDefault(x => x.Gender == "Female");
                        }
                        else
                        {
                            SelectedGender = Constants.Genders.FirstOrDefault(x => x.Gender == "Male");
                        }


                        IsVisibleGuestInformation = true;
                        IsVisibleHint = false;
                        IsVisiblePage = true;

                        IsVisibleIndicator = false;
                        IsRunningIndicator = false;

                        //FullDocumentImge = result.FullDocumentImage;
                        //FrameImageSource = mrtdSuccessFrameGrabberRecognizer.Result.SuccessFrame;
                    }

                }

            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
