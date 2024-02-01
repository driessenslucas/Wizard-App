using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WizardApi.Models;

namespace WizardApi.Models
{
    public class Elixir
    {

        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "effect")]
        public String Effect { get; set; }

        [JsonProperty(PropertyName = "sideeffect")]
        public String SideEffect { get; set; }

        [JsonProperty(PropertyName = "characteristics")]
        public String characteristics { get; set; }

        [JsonProperty(PropertyName = "time")]
        public String Time { get; set; }

        [JsonProperty(PropertyName = "difficulty")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ElixirDifficulty difficulty { get; set; }

        [JsonProperty(PropertyName = "ingredients")]
        public Ingredient[] Ingredients { get; set; }

        [JsonProperty(PropertyName = "inventors")]
        public ElixirInventor[] Inventors { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public String Manufacturer { get; set; }

    }
    public class Ingredient
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }
    }
    public class ElixirInventor
    {
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public string name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
    public enum ElixirDifficulty
    {
        Unknown, Advanced, Moderate, Beginner, OrdinaryWizardingLevel, OneOfAKind
    }

}

