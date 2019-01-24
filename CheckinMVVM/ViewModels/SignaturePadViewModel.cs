using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Globals;
using CheckinMVVM.Models;
using SignaturePad.Forms;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class SignaturePadViewModel : INotifyPropertyChanged
    {

        public ICommand OnPageApperaringCommand { get; }
        public ICommand SaveButtonPressedCommand { get; }
        public ICommand CloseButtonPressedCommand { get; }

        private INavigation Navigation;

        private bool _isAgreementToggled = false;
        public bool IsAgreementToggled
        {
            get
            {
                return _isAgreementToggled;
            }

            set
            {
                _isAgreementToggled = value;
                OnPropertyChanged("IsAgreementToggled");
            }
        }

        private GuestSignatureModel _selectedSignature = Constants.SelectedGuestSignature;
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


        public SignaturePadViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            OnPageApperaringCommand = new Command(PageOnLoad);
            SaveButtonPressedCommand = new Command<SignaturePadView>(async (obj)=> await SaveButtonPressed(obj));
            CloseButtonPressedCommand = new Command(CloseButtonPressed);
        }

        private void CloseButtonPressed()
        {

        }

        private async Task SaveButtonPressed(SignaturePadView signaturePad)
        {
            Stream signatureStream = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Png);


            if (!(signatureStream is MemoryStream signatureMemoryStream))
            {
                signatureMemoryStream = new MemoryStream();
                signatureStream.CopyTo(signatureMemoryStream);
            }

            //Adding memorystream into a byte array
            var byteArray = signatureMemoryStream.ToArray();

            //Converting byte array into Base64 string
            var base64String = Convert.ToBase64String(byteArray);

            if(base64String!=null)
            {
                var res = await Application.Current.MainPage.DisplayAlert("Thank you!", "You have signed to the agreement successfully.", "OK","Cancel");
                if(res)
                {
                    if (!Constants.SignaturesList.Any(x=>x.GuestNumber == Constants.SelectedGuestSignature.GuestNumber && x.IsSignatureAdded == true))
                    {
                        Constants.SignaturesList.Add(new GuestSignatureModel
                        {
                            GuestNumber = Constants.SelectedGuestSignature.GuestNumber,
                            GuestName = Constants.SelectedGuestSignature.GuestName,
                            GuestNameColor = "White",
                            GuestSignatureImage = ImageSource.FromStream(() => new MemoryStream(byteArray)),
                            IsSignatureAdded = true,
                            CellColor = "#6D2276"
                        });
                    }

                    await this.Navigation.PopAsync();
                }

            }
        }

        private void PageOnLoad()
        {
            //layout.IsVisible = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
