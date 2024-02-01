using System;
using Newtonsoft.Json;

namespace wizardAPI.Models
{
    public class Wizard
    {
        [JsonProperty(PropertyName = "elixirs")]
        public WizardApi.Models.Elixir[] Elixirs { get; set; }

        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
    }
}
