using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CheckinMVVM.Globals;
using CheckinMVVM.Models;

namespace CheckinMVVM.ViewModels
{
    public class RemarksViewModel : INotifyPropertyChanged
    {
        public ReservationDetailsModel ReservationDetails { get; } = Constants.SelectedReservationDetailSet;

        public RemarksViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
