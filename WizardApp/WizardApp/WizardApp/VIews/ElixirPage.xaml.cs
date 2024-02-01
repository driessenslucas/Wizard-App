using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WizardApp.Models;
using Xamarin.Forms;
using WizardApp.Repositories;
using WizardApp.VIews;
using Plugin.Connectivity;
using System.Threading;

namespace WizardApp.VIews
{
    public partial class ElixirPage : ContentPage
    {
        bool isBusy = true;
        public ElixirPage()
        {
            InitializeComponent();
            loading.IsRunning = true;
            pickDifficulty.Title = "Difficulty";
            loadElixirs();
        }
        


        private async Task loadElixirs()
        {
            pickDifficulty.SelectedIndex = 0;
            Title = "Elixirs";
            var list =  await WizardRepository.GeElixirs();
            list.Sort();
            lvwElixirs.ItemsSource = list;
            loading.IsRunning = false;
            loading.IsVisible = false;
            
        }

        async void lvwElixirs_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Elixir listSelected = lvwElixirs.SelectedItem as Elixir;
            if (listSelected != null)
            {
                await Navigation.PushAsync(new ElixirDetailsPage(listSelected));
                lvwElixirs.SelectedItem = null;
            }
        }
        async void pickDifficulty_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1 && selectedIndex != 0)
            {
                String diff = picker.Items[selectedIndex];
                var list = await WizardRepository.GeElixirsByDifficulty(diff);
                list.Sort();
                lvwElixirs.ItemsSource = list;
                Title = $"{diff} elixirs";
            }
            else if(selectedIndex == 0)
            {
                var list = await WizardRepository.GeElixirs();
                list.Sort();
                lvwElixirs.ItemsSource = list;
                Title = "Elixirs";
            }

           
        }

        async void addElixirButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

      
    }
}

