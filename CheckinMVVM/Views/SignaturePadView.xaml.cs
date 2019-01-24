using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class SignaturePadView : ContentPage
    {
        private SignaturePadViewModel _signaturePadViewModel;

        public SignaturePadView()
        {
            InitializeComponent();
            _signaturePadViewModel = new SignaturePadViewModel(Navigation);
            BindingContext = _signaturePadViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _signaturePadViewModel.OnPageApperaringCommand.Execute(SignaturePad);
        }
    }
}
