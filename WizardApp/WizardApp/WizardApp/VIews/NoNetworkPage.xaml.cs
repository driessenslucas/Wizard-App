using System;
using System.Collections.Generic;
using Plugin.Connectivity;
using Xamarin.Forms;


namespace WizardApp.VIews
{
    public partial class NoNetworkPage : ContentPage
    {
        public NoNetworkPage()
        {
            InitializeComponent();
            noInternetImg.Source = ImageSource.FromResource("WizardApp.Assets.dino.jpg");
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {

            }
            else
            {
                //await Navigation.PushAsync(new ElixirPage());
               await Application.Current.MainPage.Navigation.PopAsync();
            }

        }
    }
}

