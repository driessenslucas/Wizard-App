using System;
using System.Collections.Generic;
using System.Diagnostics;
using WizardApp.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Text;
using WizardApp.Models;

namespace WizardApp.Repositories
{
    public class WizardRepository
    {
        private const string _USERTOKEN = "\n";

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");

            return client;
        }

        public static async Task<List<Elixir>> GeElixirs()
        {
            String url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    var data = JsonConvert.DeserializeObject<List<Elixir>>(json);

                    List<Elixir> elixirs = data;

                    //Thread.Sleep(1000);
                    return elixirs;

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting elixirs, {e}");
                    throw e;
                }
            }
        }

        public static async Task<Elixir> GeElixirByName(string name)
        {
            String url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    var data = JsonConvert.DeserializeObject<List<Elixir>>(json);
                    List<Elixir> elixirs = data;
                    
                    Elixir elixir = new Elixir();

                    elixir = elixirs.Find(item => (item.Name == name));

                    //Thread.Sleep(1000);
                    if (elixir != null)
                    {
                        return elixir;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting elixir with name {name}, {e}");
                    throw e;
                }
            }
        }



        public static async Task<List<Ingredient>> GetIngredients()
        {
            String url = $"https://wizardapilucas.azurewebsites.net/api/ingredients/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    List<Ingredient> ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(json);


                    return ingredients;

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting ingredients , {e}");
                    throw e;
                }
            }
        }
        public static async Task<List<Elixir>> GeElixirsByDifficulty(String difficulty)
        {
            String url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/{difficulty}/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    List<Elixir> elixirs = JsonConvert.DeserializeObject<List<Elixir>>(json);


                    return elixirs;

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting elixirs with difficulty {difficulty}, {e}");
                    throw e;
                }
            }
        }

        public static async Task<List<Wizard>> GetWizards()
        {
            String url = $"https://wizardapilucas.azurewebsites.net/api/wizards/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    List<Wizard> wizards = JsonConvert.DeserializeObject<List<Wizard>>(json);


                    return wizards;

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting wizards , {e}");
                    throw e;
                }
            }
        }

        public static async Task<int> AddELixir(Elixir elixir)
        {
            string url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(elixir);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errormsg = $"Unsuccesful Post to url: {url}, object: {json}";
                        Debug.WriteLine(errormsg);
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return -1;
                }
            }
        }
        public static async Task<int> EditElixir(Elixir elixir)
        {
            string url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(elixir);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errormsg = $"Unsuccesful Put to url: {url}, object: {json}";
                        Debug.WriteLine(errormsg);
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return -1;
                }
            }
        }
        
        public static async Task<List<Spell>> GetSpellsAsync()
        {
            String url = $"https://wizardapilucas.azurewebsites.net/api/spells/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    var data = JsonConvert.DeserializeObject<List<Spell>>(json);

                    List<Spell> spells = data;

                    //Thread.Sleep(1000);
                    return spells;

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting spells, {e}");
                    throw e;
                }
            }
        }

        public static async Task<int> AddSpell(Spell spell)
        {
            string url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(spell);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errormsg = $"Unsuccesful Post to url: {url}, object: {json}";
                        Debug.WriteLine(errormsg);
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return -1;
                }
            }
        }

        public static async Task<int> EditSpell(Spell spell)
        {

            string url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(spell);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errormsg = $"Unsuccesful Put to url: {url}, object: {json}";
                        Debug.WriteLine(errormsg);
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return -1;
                }
            }
        }
        
        public static async Task<List<Wizard>> GetWizardsAsync()
        {
            String url = $"https://wizardapilucas.azurewebsites.net/api/wizards/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    var data = JsonConvert.DeserializeObject<List<Wizard>>(json);

                    List<Wizard> spells = data;

                    //Thread.Sleep(1000);
                    return spells;

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting wizards, {e}");
                    throw e;
                }
            }
        }

        public static async Task<int> AddWizard(Wizard wizard)
        {
            string url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(wizard);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errormsg = $"Unsuccesful Post to url: {url}, object: {json}";
                        Debug.WriteLine(errormsg);
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return -1;
                }
            }
        }

        public static async Task<int> EditWizard(Wizard wizard)
        {
            
            string url = $"https://wizardapilucas.azurewebsites.net/api/elixirs/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(wizard);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errormsg = $"Unsuccesful Put to url: {url}, object: {json}";
                        Debug.WriteLine(errormsg);
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return -1;
                }
            }
        }

        public static async Task<List<Wizard>> UpdateWizardElixirs()
        {
            string url = $"https://wizardapilucas.azurewebsites.net/api/Updatewizards/";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    var data = JsonConvert.DeserializeObject<List<Wizard>>(json);

                    List<Wizard> elixirs = data;

                    //Thread.Sleep(1000);
                    return elixirs;

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error updating wizard elixirs, {e}");
                    throw e;
                }
            }
        }

    }

}

