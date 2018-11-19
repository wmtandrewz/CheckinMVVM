using System;
using System.Collections.Generic;
using CheckinMVVM.Models;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class MainActivityView : ContentPage
    {
        MainActivityViewModel mainActivityViewModel;

        public MainActivityView(ReservationsHeaderModel reservationsHeader) 
        {
            InitializeComponent();

            mainActivityViewModel = new MainActivityViewModel(reservationsHeader,Navigation);
            BindingContext = mainActivityViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainActivityViewModel.PageOnLoadCommand.Execute(null);
        }
    }
}
