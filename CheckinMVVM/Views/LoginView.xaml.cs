using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();
        }
    }
}
