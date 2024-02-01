using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Connectivity;
using WizardApp.Models;
using WizardApp.Repositories;
using Xamarin.Forms;

namespace WizardApp.VIews
{
    public partial class SpellDetailsPage : ContentPage
    {
        Spell globSpell;
        public SpellDetailsPage(Spell spell)
        {
            InitializeComponent();
            globSpell = spell;
            loadDetails();
        }


        private async Task loadDetails()
        {

            lblCanBeVerbal.Text = globSpell.CanBeVerbal.ToString();
        
            lblName.Text = globSpell.Name.ToString();
            lblType.Text = globSpell.Type.ToString();
            lblLight.Text = globSpell.Light.ToString();

            if (globSpell.Creator != null)
            {
                lblCreator.Text = globSpell.Creator.ToString();
            }
            else
            {
                lblCreator.Text = "Unknown";
            }
            

        }
        async void btnEdit_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ListPages());

        }
        async void btnBack_Clicked(System.Object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}

