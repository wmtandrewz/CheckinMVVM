using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class RegistrationCardView : ContentPage
    {
        private RegistrationCardViewModel _registrationCardViewModel;

        public RegistrationCardView()
        {
            InitializeComponent();

            _registrationCardViewModel = new RegistrationCardViewModel();
            BindingContext = _registrationCardViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _registrationCardViewModel.PageOnLoadCommand.Execute(null);
        }
    }
}
