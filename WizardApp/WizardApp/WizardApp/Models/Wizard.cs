using System;
using Newtonsoft.Json;

namespace WizardApp.Models
{
    public class Wizard
    {
        [JsonProperty(PropertyName = "elixirs")]
        public WizardElixir[] Elixirs { get; set; }

        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        public string Name
        {
            get
            {
                return $"{this.FirstName} " + " " + $"{this.LastName}";
            }
        }
        public int NrOfElixirs
        {
            get
            {
                return this.Elixirs.Length;
            }
        }
    }
    public class WizardElixir
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
