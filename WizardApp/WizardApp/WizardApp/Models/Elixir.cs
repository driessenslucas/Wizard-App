using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WizardApp.Models;

namespace WizardApp.Models
{
    public class Elixir: IComparable
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

        public int IngredientsLength
        {
            get
            {
                return this.Ingredients.Length;
            }
        }

       
        public int CompareTo(object obj)
        {
            Elixir obj2 = (Elixir)obj;
            try
            {
                {
                    return this.Name.CompareTo(obj2.Name);
                }

            }
            catch (Exception e)
            {
                return 0;
            }
           
        }
    }
    public class Ingredient
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }
    public class ElixirInventor
    {
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public string Name
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

