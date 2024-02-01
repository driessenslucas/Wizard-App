using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WizardApp.Models;
using WizardApp.Repositories;
using Xamarin.Forms;

namespace WizardApp.VIews
{
    public partial class EditPage : ContentPage
    {
        Elixir globElixir;
        List<SelectableData<Ingredient>> DataListIngredients = new List<SelectableData<Ingredient>>();
        List<SelectableData<Wizard>> DataListWizards = new List<SelectableData<Wizard>>();
        public EditPage(Elixir elixir)
        {
            InitializeComponent();
            globElixir = elixir;

            Title = $"editing {elixir.Name}";
            loadItems();
        }
        

        async Task loadItems()
        {
            await loadIngredients();
            await loadWizards();
            editName.Text = globElixir.Name;
            editEffect.Text = globElixir.Effect;
            int pickerint = (int)globElixir.difficulty;
            pickDifficulty.SelectedIndex = pickerint;
            editSideEffect.Text = globElixir.SideEffect;
            editCharacteristics.Text = globElixir.characteristics;
            editTime.Text = globElixir.Time;
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
                if (data.Selected == true)
                {
                    var obj = (Ingredient)data.Data;
                    list.Add(obj);
                }

            return list;
        }
        async Task loadIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.AddRange(globElixir.Ingredients);
            List<SelectableData<Ingredient>> selectedIngredients = new List<SelectableData<Ingredient>>();
            List<SelectableData<Ingredient>> unselectedIngredients = new List<SelectableData<Ingredient>>();
            foreach (var item in ingredients)
            {
                selectedIngredients.Add(new SelectableData<Ingredient>() { Data = item, Selected = true });
            }
            var allIngredients = await WizardRepository.GetIngredients();
            foreach (var item in allIngredients)
            {
                if (!ingredients.Contains(item))
                {
                    unselectedIngredients.Add(new SelectableData<Ingredient>() { Data = item, Selected = false });
                }
            }
            DataListIngredients = selectedIngredients;
            DataListIngredients.AddRange(unselectedIngredients);
            lvmIngredients.ItemsSource = DataListIngredients;
            
        }
        async Task loadWizards()
        {
            List<Wizard> wizards = new List<Wizard>();
            foreach (var item in globElixir.Inventors)
            {
                Wizard newwizard = new Wizard();
                newwizard.LastName = item.LastName;
                newwizard.Id = item.Id;
                newwizard.FirstName = item.FirstName;
                wizards.Add(newwizard);
            }

            List<SelectableData<Wizard>> selectedWizards = new List<SelectableData<Wizard>>();
            List<SelectableData<Wizard>> unselectedWizards = new List<SelectableData<Wizard>>();
            foreach (var item in wizards)
            {
                selectedWizards.Add(new SelectableData<Wizard>() { Data = item, Selected = true });
            }
            var allWizards = await WizardRepository.GetWizards();
            foreach (var item in allWizards)
            {
                if (!wizards.Contains(item))
                {
                    unselectedWizards.Add(new SelectableData<Wizard>() { Data = item, Selected = false });
                }
            }
            DataListWizards = selectedWizards;
            DataListWizards.AddRange(unselectedWizards);
            lvmInventors.ItemsSource = DataListWizards;
        }
        async void btnSave_Clicked(System.Object sender, System.EventArgs e)
        {
            //get all data from fields
            ElixirDifficulty diff = new ElixirDifficulty();
            Elixir newElixir = globElixir;
            var ingredientList = GetSelectedIngredients();
            var invetorsList = GetSelectedWizards();
            newElixir.Ingredients = ingredientList.ToArray();
            newElixir.Inventors = invetorsList.ToArray();
            if (editName.Text != null) { newElixir.Name = editName.Text; };
            if (editEffect.Text != null) { newElixir.Effect = editEffect.Text; };
            if (pickDifficulty.SelectedIndex > -1)
            {
                newElixir.difficulty = (ElixirDifficulty)Enum.Parse(typeof(ElixirDifficulty), pickDifficulty.Items[pickDifficulty.SelectedIndex], true);
            }
            else
            {
                newElixir.difficulty = ElixirDifficulty.Unknown;
            }
            if (editSideEffect.Text != null) { newElixir.SideEffect = editSideEffect.Text; };
            if (editTime.Text != null) { newElixir.Time = editTime.Text; };
            if (editCharacteristics.Text != null) { newElixir.characteristics = editCharacteristics.Text; };
            var status = await WizardRepository.EditElixir(newElixir);

            if(status != -1)
            {
                await WizardRepository.UpdateWizardElixirs();
                await Navigation.PopAsync();
            }
        }
        async void btnCancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }



    }
}

