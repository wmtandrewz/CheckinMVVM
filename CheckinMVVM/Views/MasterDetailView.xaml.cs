using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class MasterDetailView : MasterDetailPage
    {
        MasterDetailViewModel masterDetailViewModel;

        public MasterDetailView()
        {
            InitializeComponent();

            masterDetailViewModel = new MasterDetailViewModel(this);
            BindingContext = masterDetailViewModel;

            this.Master = new ReservationsListView
            {
                Title = "Menu",
                Icon = "Icons/drawer_icon.png"
            };

            this.MasterBehavior = MasterBehavior.SplitOnLandscape;

            this.Detail = new NavigationPage(new HomeView())
            {
                BarBackgroundColor = Color.FromHex("#6D2276"),
                BarTextColor = Color.White
            };
        }

    }
}
