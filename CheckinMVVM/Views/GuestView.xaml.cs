using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Microblink.Forms.Core;
using Microblink.Forms.Core.Overlays;
using Microblink.Forms.Core.Recognizers;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class GuestView : ContentPage
    {
        private GuestViewModel guestViewModel;

        public GuestView()
        {
            InitializeComponent();
            guestViewModel = new GuestViewModel(Navigation);
            BindingContext = guestViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            guestViewModel.LoadGuestProfilesCommand.Execute(null);
        }

    }
}
