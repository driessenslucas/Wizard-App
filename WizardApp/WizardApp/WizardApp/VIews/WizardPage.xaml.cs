using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardApp.Models;
using WizardApp.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WizardApp.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WizardPage : ContentPage
    {
        bool isBusy = true;
        public WizardPage()
        {
            InitializeComponent();
            loading.IsRunning = true;
            loadItems();
        }
        

        async Task loadItems()
        {
            lvmWizards.ItemsSource = await WizardRepository.GetWizardsAsync();
            loading.IsRunning = false;
            loading.IsVisible = false;
        }

        async void lvmWizards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Wizard listSelected = lvmWizards.SelectedItem as Wizard;
            if (listSelected != null)
            {
                await Navigation.PushAsync(new WizardDetailPage(listSelected));
                lvmWizards.SelectedItem = null;
            }
        }

        //private void addWizardButton_Clicked(object sender, EventArgs e)
        //{

        //}

        
    }
}