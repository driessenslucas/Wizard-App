using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using WizardApp.Models;
using WizardApp.Repositories;
using Plugin.Connectivity;
using System.Threading.Tasks;

namespace WizardApp.VIews
{
    public partial class WizardChart : ContentPage
    {
        public WizardChart()
        {
            InitializeComponent();

            loadItems();
        }



        async Task loadItems()
        {
            String[] colors = { "#DFFF00", "#FFBF00", "#FF7F50" , "#DE3163", "#9FE2BF" , "#40E0D0", "#CCCCFF", "#6495ED" };
            Random rnd = new Random();
            var WizardElixirs = await WizardRepository.GetWizards();
            List<ChartEntry> newEntries = new List<ChartEntry>();
            foreach (var item in WizardElixirs)
            {
                if (item.NrOfElixirs > 0)
                {
                    ChartEntry newEntry = new ChartEntry(item.NrOfElixirs) { Label= item.Name, Color= SKColor.Parse(colors[rnd.Next(0, colors.Length)]), ValueLabel= item.NrOfElixirs.ToString() };
                    newEntries.Add(newEntry);
                }
            }
            chartViewBar.Chart = new BarChart { Entries = newEntries.ToArray(), LabelTextSize = 32, Margin = 8, IsAnimated=true, ValueLabelOption= ValueLabelOption.OverElement , ValueLabelOrientation = Orientation.Vertical};
        }


       


    }
}

