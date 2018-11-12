using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Helpers;
using CheckinMVVM.Services;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
	public class HomeViewModel : INotifyPropertyChanged
    {
        public ICommand PageOnLoadCommand { get; }

        public HomeViewModel()
        {
            PageOnLoadCommand = new Command(async() => await PageOnLoad());
        }

        private async Task PageOnLoad()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
