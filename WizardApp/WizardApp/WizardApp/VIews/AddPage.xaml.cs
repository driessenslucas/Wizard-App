using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WizardApp.Repositories;
using Xamarin.Forms;
using WizardApp.Models;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Plugin.Connectivity;

namespace WizardApp.VIews
{
    public partial class AddPage : ContentPage
    {
        //edit no network
        List<SelectableData<Ingredient>> DataListIngredients = new List<SelectableData<Ingredient>>();
        List<SelectableData<Wizard>> DataListWizards = new List<SelectableData<Wizard>>();
        public AddPage()
        {
            InitializeComponent();
            loadItems();
        }
        

        async Task loadItems()
        {
            loadIngredients();
            loadWizards();
        }
       
        private List<ElixirInventor> GetSelectedWizards()
        {
            var list = new List<ElixirInventor>();

            foreach (var data in DataListWizards)
                if (data.Selected == true)
                {
                    var obj = (Wizard)data.Data;
                    ElixirInventor inventor = new ElixirInventor();
                    inventor.Id = obj.Id;
                    inventor.FirstName = obj.FirstName;
                    inventor.LastName = obj.LastName;
                    
                    list.Add(inventor);
                }

            return list;
        }


        private List<Ingredient> GetSelectedIngredients()
        {
            var list = new List<Ingredient>();

            foreach (var data in DataListIngredients)
                if (data.Selected == true) {
                    var obj = (Ingredient)data.Data;
                    list.Add(obj);
                }

            return list;
        }


        async void loadIngredients()
        {
            List<Ingredient> ingredients = await WizardRepository.GetIngredients();
          
            List<SelectableData<Ingredient>> selectedIngredients = new List<SelectableData<Ingredient>>();
            foreach (var item in ingredients)
            {
                selectedIngredients.Add(new SelectableData<Ingredient>() { Data = item, Selected = false });
            }
            DataListIngredients = selectedIngredients;
            lvmIngredients.ItemsSource = DataListIngredients;
        }


        async void loadWizards()
        {
            List<Wizard> wizards = await WizardRepository.GetWizards();
           
            List<SelectableData<Wizard>> selectedWizards = new List<SelectableData<Wizard>>();
            foreach (var item in wizards)
            {
                selectedWizards.Add(new SelectableData<Wizard>() { Data = item, Selected = false });
            }
            DataListWizards = selectedWizards;
            lvmInventors.ItemsSource = DataListWizards;
        }


        async void btnSave_Clicked(System.Object sender, System.EventArgs e)
        {
            //get all data from fields
            ElixirDifficulty diff = new ElixirDifficulty();

            Elixir newElixir = new Elixir();
            var ingredientList = GetSelectedIngredients();
            var invetorsList = GetSelectedWizards();
            newElixir.Ingredients = ingredientList.ToArray();
            newElixir.Inventors = invetorsList.ToArray();
            newElixir.Name = EditName.Text;
            newElixir.Effect = editEffect.Text;
            if (pickDifficulty.SelectedIndex > -1)
            {
                newElixir.difficulty = (ElixirDifficulty)Enum.Parse(typeof(ElixirDifficulty), pickDifficulty.Items[pickDifficulty.SelectedIndex], true);
            }
            else {
                newElixir.difficulty = ElixirDifficulty.Unknown;
            }
            newElixir.SideEffect = editSideEffect.Text;
            newElixir.Time = editTime.Text;
            newElixir.characteristics = editCharacteristics.Text;

            var status =  await WizardRepository.AddELixir(newElixir);
            if (status != -1)
            {
                await WizardRepository.UpdateWizardElixirs();
                await Navigation.PushAsync(new ListPages());
            }
        }


        async void btnCancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}

