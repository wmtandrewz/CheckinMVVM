﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CheckinMVVM.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CheckinMVVM
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
