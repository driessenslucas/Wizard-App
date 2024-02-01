using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using WizardApi.Models;
using System.Diagnostics;
using Microsoft.Azure.Cosmos;
using Azure.Security.KeyVault;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using wizardAPI.Models;

namespace MCT.Function
{
    public static class WizardAPI
    {
        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");

            return client;
        }
        [FunctionName("GetElixirs")]
        public static async Task<IActionResult> getElixirs(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "elixirs")] HttpRequest req,
            ILogger log)
        {


            try
            {
                List<Elixir> elixirs = new List<Elixir>();
                try
                {
                    // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                    // var secret = await secretClient.GetSecretAsync("cosmosuri");

                    CosmosClientOptions options = new CosmosClientOptions()
                    {
                        ConnectionMode = ConnectionMode.Gateway
                    };
                    var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                    var container = cosmosClient.GetContainer("WizardDb", "elixirs");
                    string sql = $"SELECT * FROM c";
                    var query = container.GetItemQueryIterator<Elixir>(sql);
                    List<Elixir> elixirsFromCosmos = new List<Elixir>();
                    while (query.HasMoreResults)
                    {
                        var response = await query.ReadNextAsync();
                        elixirsFromCosmos.AddRange(response);
                    }
                    foreach (var item in elixirsFromCosmos)
                    {
                        elixirs.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    log.LogError(ex.Message);
                }
                return new OkObjectResult(elixirs);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"error getting elixirs, {e}");
                return new BadRequestObjectResult(e.Message);
            }
        }



        [FunctionName("GetElixirsByDifficulty")]
        public static async Task<IActionResult> getElixirsByDifficulty(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "elixirs/{difficulty}")] HttpRequest req, string difficulty,
            ILogger log)
        {
            String url = $"https://wizard-world-api.herokuapp.com/Elixirs?Difficulty={difficulty}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    String json = await client.GetStringAsync(url);

                    List<Elixir> elixirs = JsonConvert.DeserializeObject<List<Elixir>>(json);
                    try
                    {
                        // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                        // var secret = await secretClient.GetSecretAsync("cosmosuri");

                        CosmosClientOptions options = new CosmosClientOptions()
                        {
                            ConnectionMode = ConnectionMode.Gateway
                        };
                        var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                        var container = cosmosClient.GetContainer("WizardDb", "elixirs");
                        string sql = $"SELECT * FROM c where c.difficulty like '{difficulty}'";
                        var query = container.GetItemQueryIterator<Elixir>(sql);
                        List<Elixir> elixirsFromCosmos = new List<Elixir>();
                        while (query.HasMoreResults)
                        {
                            var response = await query.ReadNextAsync();
                            elixirsFromCosmos.AddRange(response);
                        }
                        foreach (var item in elixirsFromCosmos)
                        {
                            elixirs.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.LogError(ex.Message);
                    }
                    return new OkObjectResult(elixirs);

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error getting elixirs with difficulty {difficulty}, {e}");
                    return new BadRequestObjectResult(e.Message);
                }
            }
        }

        [FunctionName("GetElixirsById")]
        public static async Task<IActionResult> getElixirsById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "elixirsId/{Id}")] HttpRequest req, string Id,
            ILogger log)
        {

            Elixir elixirs = new Elixir();
            try
            {
                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");

                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                var container = cosmosClient.GetContainer("WizardDb", "elixirs");
                string sql = $"SELECT * FROM c where c.id like '{Id}'";
                var query = container.GetItemQueryIterator<Elixir>(sql);
                List<Elixir> elixirsFromCosmos = new List<Elixir>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    elixirsFromCosmos.AddRange(response);
                }
                foreach (var item in elixirsFromCosmos)
                {
                    elixirs = item;
                }
                return new OkObjectResult(elixirs);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"error getting elixirs with id: {Id}, {e}");
                return new BadRequestObjectResult(e.Message);
            }
        }




        [FunctionName("AddElixir")]
        public static async Task<IActionResult> addElixir(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "elixirs")] HttpRequest req,
            ILogger log)
        {
            String json = await new StreamReader(req.Body).ReadToEndAsync();

            try
            {
                Elixir elixir = JsonConvert.DeserializeObject<Elixir>(json);

                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");

                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };

                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);


                var container = cosmosClient.GetContainer("WizardDb", "elixirs");
                elixir.Id = Guid.NewGuid().ToString();
                await container.CreateItemAsync<Elixir>(elixir, new PartitionKey(elixir.Id));
                return new OkObjectResult(elixir);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"error adding elixir, {e}");
                return new BadRequestObjectResult(e.Message);
            }
        }

        [FunctionName("EditElixir")]
        public static async Task<IActionResult> editElixir(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "elixirs")] HttpRequest req,
            ILogger log)
        {
            String json = await new StreamReader(req.Body).ReadToEndAsync();

            try
            {
                Elixir elixir = JsonConvert.DeserializeObject<Elixir>(json);


                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };

                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);


                var container = cosmosClient.GetContainer("WizardDb", "elixirs");

                await container.ReplaceItemAsync<Elixir>(elixir, elixir.Id);
                return new OkObjectResult(elixir);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"error adding elixir, {e}");
                return new BadRequestObjectResult(e.Message);
            }

        }

        // spells
        [FunctionName("GetSpells")]
        public static async Task<IActionResult> getSpells(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "spells")] HttpRequest req,
            ILogger log)
        {
            try
            {
                List<Spell> spells = new List<Spell>();
                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");

                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                var container = cosmosClient.GetContainer("WizardDb", "spells");
                string sql = $"SELECT * FROM c";
                var query = container.GetItemQueryIterator<Spell>(sql);
                List<Spell> spellsFromCosmos = new List<Spell>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    spellsFromCosmos.AddRange(response);
                }
                foreach (var item in spellsFromCosmos)
                {
                    spells.Add(item);
                }

                return new OkObjectResult(spells);
            }

            catch (Exception e)
            {
                Debug.WriteLine($"error getting spells, {e}");
                return new BadRequestObjectResult(e.Message);
            }
        }

        [FunctionName("GetSpellsbyType")]
        public static async Task<IActionResult> getSpellsByType(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "spells/{type}")] HttpRequest req, string type,
           ILogger log)
        {
            try
            {
                List<Spell> spells = new List<Spell>();

                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");

                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                var container = cosmosClient.GetContainer("WizardDb", "spells");
                string sql = $"SELECT * FROM c where c.type like '{type}'";
                var query = container.GetItemQueryIterator<Spell>(sql);
                List<Spell> spellsFromCosmos = new List<Spell>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    spellsFromCosmos.AddRange(response);
                }
                foreach (var item in spellsFromCosmos)
                {
                    spells.Add(item);
                }
                return new OkObjectResult(spells);
            }

            catch (Exception e)
            {
                Debug.WriteLine($"error getting spells with type {type}, {e}");
                return new BadRequestObjectResult(e.Message);
            }
        }


        [FunctionName("AddSpell")]
        public static async Task<IActionResult> addSpell(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "spells")] HttpRequest req,
            ILogger log)
        {
            try
            {
                String json = await new StreamReader(req.Body).ReadToEndAsync();

                Spell spell = JsonConvert.DeserializeObject<Spell>(json);

                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");

                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };

                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);



                var container = cosmosClient.GetContainer("WizardDb", "spells");
                spell.Id = Guid.NewGuid().ToString();
                await container.CreateItemAsync<Spell>(spell, new PartitionKey(spell.Id));
                return new OkObjectResult(spell);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"error adding spell, {e}");
                return new BadRequestObjectResult(e.Message);
            }

        }

        // wizards

        [FunctionName("GetWizards")]
        public static async Task<IActionResult> getWizards(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "wizards")] HttpRequest req,
            ILogger log)
        {
            try
            {

                List<Wizard> wizards = new List<Wizard>();

                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");

                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                var container = cosmosClient.GetContainer("WizardDb", "wizards");
                string sql = $"SELECT * FROM c";
                var query = container.GetItemQueryIterator<Wizard>(sql);
                List<Wizard> wizardsFromCosmos = new List<Wizard>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    wizardsFromCosmos.AddRange(response);
                }

                return new OkObjectResult(wizardsFromCosmos);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"error getting wizards, {e}");
                return new BadRequestObjectResult(e.Message);
            }
        }

        [FunctionName("GetIngredients")]
        public static async Task<IActionResult> getIngredients(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ingredients")] HttpRequest req,
            ILogger log)
        {

            try
            {

                List<Ingredient> ingredients = new List<Ingredient>();


                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");

                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                var container = cosmosClient.GetContainer("WizardDb", "ingredients");
                string sql = $"SELECT * FROM c";
                var query = container.GetItemQueryIterator<Ingredient>(sql);
                List<Ingredient> ingreidientsFromCosmos = new List<Ingredient>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    ingreidientsFromCosmos.AddRange(response);
                }


                return new OkObjectResult(ingreidientsFromCosmos);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"error getting ingredients , {e}");
                throw e;
            }
        }

        [FunctionName("updateWizardElixirs")]
        public static async Task<IActionResult> updateWizardElixirs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Updatewizards")] HttpRequest req,
            ILogger log)
        {
            try
            {

                // var secretClient = new SecretClient(new Uri("https://eindwerkvault.vault.azure.net/"), new DefaultAzureCredential());
                // var secret = await secretClient.GetSecretAsync("cosmosuri");


                CosmosClientOptions options = new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                var cosmosClient = new CosmosClient("AccountEndpoint=https://lucasdriessens-cosmos.documents.azure.com:443/;AccountKey=jOdHgImyedUopWvNk7jdXEHBnLYyQQM9NefvQhnLaDi7hbPxaz6fZvM0Br5j8vWvYaSe8byfhE3Ydz8uGQMFiw==;", options);

                var container = cosmosClient.GetContainer("WizardDb", "wizards");



                //get wizards from cosmos

                var query = container.GetItemQueryIterator<Wizard>("select * from c");
                List<Wizard> wizards = new List<Wizard>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    wizards.AddRange(response);
                }
                foreach (var wizard in wizards)
                {
                    if (wizard.FirstName == null || wizard.FirstName == "")
                    {
                        string sql = $"SELECT c.id,c.name,c.effect,c.sideeffect,c.characteristics,c.time,c.difficulty,c.ingredients,c.inventors FROM c join t in c.inventors where t.name like '%{wizard.LastName}'";
                        var containerElixirs = cosmosClient.GetContainer("WizardDb", "elixirs");
                        var qry = containerElixirs.GetItemQueryIterator<Elixir>(sql);
                        List<Elixir> elixirs = new List<Elixir>();
                        while (qry.HasMoreResults)
                        {
                            var response = await qry.ReadNextAsync();
                            elixirs.AddRange(response);
                        }
                        wizard.Elixirs = elixirs.ToArray();
                        await container.ReplaceItemAsync<Wizard>(wizard, wizard.Id);
                    }
                    else
                    {
                        string sql = $"SELECT c.id,c.name,c.effect,c.sideeffect,c.characteristics,c.time,c.difficulty,c.ingredients,c.inventors FROM c join t in c.inventors where t.name like '{wizard.FirstName} {wizard.LastName}'";
                        var containerElixirs = cosmosClient.GetContainer("WizardDb", "elixirs");
                        var qry = containerElixirs.GetItemQueryIterator<Elixir>(sql);
                        List<Elixir> elixirs = new List<Elixir>();
                        while (qry.HasMoreResults)
                        {
                            var response = await qry.ReadNextAsync();
                            elixirs.AddRange(response);
                        }
                        wizard.Elixirs = elixirs.ToArray();
                        await container.ReplaceItemAsync<Wizard>(wizard, wizard.Id);
                    }

                }


                return new OkObjectResult(wizards);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"error updating wizard elixirs, {e}");
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
