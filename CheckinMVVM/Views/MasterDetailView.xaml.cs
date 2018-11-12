using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class MasterDetailView : MasterDetailPage
    {
        public MasterDetailView()
        {
            InitializeComponent();

            this.Master = new ReservationsListView
            {
                Title = "Menu"
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
