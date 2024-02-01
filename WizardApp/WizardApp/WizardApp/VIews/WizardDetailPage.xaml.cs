using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardApp.Repositories;
using WizardApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WizardApp.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WizardDetailPage : ContentPage
    {
        Wizard globWizard;
        public WizardDetailPage(Wizard wizard)
        {
            InitializeComponent();
            globWizard = wizard;
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            

            if (!CrossConnectivity.Current.IsConnected)
            {

                await Navigation.PushAsync(new NoNetworkPage());
            }
            else
            {
                await loadItems();

            }
        }
        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {
                await Navigation.PushAsync(new NoNetworkPage());
            }
            else
            {
                await loadItems();
            }
        }

        private async Task loadItems()
        {
            Title = globWizard.Name;
            lblName.Text = globWizard.Name;
            lvmElixirs.ItemsSource = globWizard.Elixirs;
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListPages());
        }

        async void lvmElixirs_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {


            WizardElixir listSelected = lvmElixirs.SelectedItem as WizardElixir;
            if (listSelected != null)
            {
                Elixir item = await WizardRepository.GeElixirByName(listSelected.Name.ToString());
                await Navigation.PushAsync(new ElixirDetailsPage(item));
                lvmElixirs.SelectedItem = null;
            }
        }
    }
}