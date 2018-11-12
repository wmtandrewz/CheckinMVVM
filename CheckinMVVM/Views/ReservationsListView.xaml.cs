using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class ReservationsListView : ContentPage
    {
        ReservationsListViewModel viewModel;

        public ReservationsListView()
        {
            InitializeComponent();

            viewModel = new ReservationsListViewModel();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.ReservationsListOnLoadCommand.Execute(null);
        }
    }
}
