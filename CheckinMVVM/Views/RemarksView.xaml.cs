using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class RemarksView : ContentPage
    {
        private RemarksViewModel _remarksViewModel;

        public RemarksView()
        {
            InitializeComponent();
            _remarksViewModel = new RemarksViewModel(Navigation);
            BindingContext = _remarksViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _remarksViewModel.PageOnLoadCommand.Execute(null);
        }
    }
}
