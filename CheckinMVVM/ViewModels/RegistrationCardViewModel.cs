using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class RegistrationCardViewModel
    {

        public ICommand PageOnLoadCommand { get;}

        public RegistrationCardViewModel()
        {
            PageOnLoadCommand = new Command(PangeOnLoad);
        }

        private async void PangeOnLoad()
        {

        }
    }
}
