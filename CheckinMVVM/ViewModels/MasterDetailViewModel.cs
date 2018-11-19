using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckinMVVM.Models;
using CheckinMVVM.Views;
using Xamarin.Forms;

namespace CheckinMVVM.ViewModels
{
    public class MasterDetailViewModel : INotifyPropertyChanged
    {

        private MasterDetailPage DetailPage;

        public MasterDetailViewModel(MasterDetailPage detailPage)
        {
            this.DetailPage = detailPage;

            MessagingCenter.Subscribe<ReservationsListViewModel, ReservationsHeaderModel>(this, "SelectedReservation", (sender, selectedReservationHeader) =>
               {
                   var contentpage = new MainActivityView(selectedReservationHeader);

                   DetailPage.Detail = new NavigationPage(contentpage)
                   {
                       BarBackgroundColor = Color.FromHex("#6D2276"),
                       BarTextColor = Color.White
                   };

                   MessagingCenter.Unsubscribe<ReservationsListViewModel>(this, "SelectedReservation");
               });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
