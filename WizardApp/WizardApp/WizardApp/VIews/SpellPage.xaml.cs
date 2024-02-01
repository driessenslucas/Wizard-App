using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using WizardApp.Models;
using WizardApp.Models;
using WizardApp.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WizardApp.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpellPage : ContentPage
    {
        bool isBusy = true;
        public SpellPage()
        {
            InitializeComponent();
            loading.IsRunning = true;
            loadSpells();
        }
        

        private async Task loadSpells()
        {

            Title = "Spells";
            var list = await WizardRepository.GetSpellsAsync();
            list.Sort();
            lvmSpells.ItemsSource = list;
            loading.IsRunning = false;
            loading.IsVisible = false;

        }
       
        async void lvmSpells_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Spell listSelected = lvmSpells.SelectedItem as Spell;
            if (listSelected != null)
            {
                await Navigation.PushAsync(new SpellDetailsPage(listSelected));
                lvmSpells.SelectedItem = null;
            }
        }


    }
}