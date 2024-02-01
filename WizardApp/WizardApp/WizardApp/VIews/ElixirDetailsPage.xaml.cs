using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Connectivity;
using WizardApp.Models;
using WizardApp.VIews;
using Xamarin.Forms;

namespace WizardApp.VIews
{
    public partial class ElixirDetailsPage : ContentPage
    {
        Elixir globElixer;
        public ElixirDetailsPage(Elixir elixir)
        {
            InitializeComponent();
            globElixer = elixir;
            loadDetails();
        }
            
        private async Task loadDetails()
        {
            
            
            Title = globElixer.Name;
            lblCharacteristics.Text = globElixer.characteristics;
            
            lblDifficulty.Text = globElixer.difficulty.ToString();
            lblEffect.Text = globElixer.Effect;
            
            lblSideEffects.Text = globElixer.SideEffect;
            lblName.Text = globElixer.Name;
            if (globElixer.Ingredients.Length > 0)
            {
                lvmIngredients.ItemsSource = globElixer.Ingredients;
            }
            if (globElixer.Inventors.Length > 0)
            {
                lvmInventors.ItemsSource = globElixer.Inventors;
            }
            try
            {

                if (lblEffect.Text.Length != 0 && lblEffect.Text.Length > 40 && lblEffect != null)
                {
                    EffectFrame.HeightRequest = lblEffect.Text.Length * 10;

                }
                if (lblCharacteristics.Text.Length != 0 && lblCharacteristics.Text.Length > 60 && lblCharacteristics != null)
                {
                    CharacteristicsFrame.HeightRequest = lblCharacteristics.Text.Length * 15;

                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        async void btnHome_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ListPages());

        }
       async void btnBack_Clicked(System.Object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        
        }

        async void btnEdit_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EditPage(globElixer));
        }
    }
}

