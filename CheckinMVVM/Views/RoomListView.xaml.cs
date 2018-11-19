using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class RoomListView : ContentPage
    {
        private RoomListViewModel roomListViewModel;

        public RoomListView(INavigation navigation)
        {
            InitializeComponent();

            roomListViewModel = new RoomListViewModel(navigation);

            BindingContext = roomListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            roomListViewModel.PageOnLoadCommand.Execute(null);
        }
    }
}
