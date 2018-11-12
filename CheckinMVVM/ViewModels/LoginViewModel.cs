using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Helpers;
using CheckinMVVM.Models;
using CheckinMVVM.Services;
using CheckinMVVM.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ICommand LoginButtonPressedCommand { get; }

        private UserModel _userModel = new UserModel();
        public UserModel LoginModel
        {
            get 
            { 
                return _userModel; 
            }
            set 
            {
                _userModel = value;
                OnPropertyChanged("LoginModel");
            }
        }

        public LoginViewModel()
        {
            LoginButtonPressedCommand = new Command(async() => await LoginUser());
        }

        private async Task LoginUser()
        {
        
            try
            {            
                //Authenticate against ADFS and NW Gateway
                OAuthLogin oauthlogin = new OAuthLogin();
                LoginModel.UserName = LoginModel.UserName + "@jkintranet.com".ToLower();
                string access_token = await oauthlogin.LoginUserAsync(LoginModel);

                if(!string.IsNullOrEmpty(access_token) && access_token.StartsWith("SUCCESS", StringComparison.CurrentCulture))
                {
                    await OnSignedIn();
                    Application.Current.MainPage = new MasterDetailView();
                    LoginModel = new UserModel();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Authorization Failed!",$"{access_token}","OK");
                    LoginModel = new UserModel();
                }
            }
            catch(Exception)
            {
                LoginModel = new UserModel();
            }
        }

        private async Task OnSignedIn()
        {
            try
            {
                var response = await GetAPIservice.GetHotelCode();
                var userHotelResult = JsonConvert.DeserializeObject<UserHotelModel>(response);

                if (userHotelResult != null)
                {
                    Settings.UserName = userHotelResult.ADuserName;
                    Settings.HotelCode = userHotelResult.HotelCode;
                }
            }
            catch(Exception e)
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
