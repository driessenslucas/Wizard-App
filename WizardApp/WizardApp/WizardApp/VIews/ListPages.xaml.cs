using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WizardApp.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPages : TabbedPage
    {
        public ListPages ()
        {
            InitializeComponent();
            ChartIcon.IconImageSource = ImageSource.FromResource("WizardApp.Assets.Chats.png");
            WizardsIcon.IconImageSource = ImageSource.FromResource("WizardApp.Assets.Wizards.png");
            SpellsIcon.IconImageSource = ImageSource.FromResource("WizardApp.Assets.Spells.png");
            ElixirsIcon.IconImageSource = ImageSource.FromResource("WizardApp.Assets.Potions.png");
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
                
            }
        }
    }
}