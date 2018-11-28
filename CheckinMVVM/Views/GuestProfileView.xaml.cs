using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class GuestProfileView : ContentPage
    {
        private GuestProfileViewModel _guestProfileViewModel;
        public GuestProfileView()
        {
            InitializeComponent();

            _guestProfileViewModel = new GuestProfileViewModel();
            BindingContext = _guestProfileViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _guestProfileViewModel.PageOnLoadCommand.Execute(null);
        }
    }
}
