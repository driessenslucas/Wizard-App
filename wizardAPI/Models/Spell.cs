using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardApi.Models
{
    public class Spell
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "effect")]
        public string Effect { get; set; }

        [JsonProperty(PropertyName = "canBeVerbal", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean CanBeVerbal { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpellType Type { get; set; }

        [JsonProperty(PropertyName = "light")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpellLight Light { get; set; }

        [JsonProperty(PropertyName = "creator")]
        public string Creator { get; set; }
    }

    public enum SpellType
    {
        None, Charm, Conjuration, Spell, Transfiguration, HealingSpell, DarkCharm, Jinx, Curse, MagicalTransportation, Hex, CounterSpell, DarkArts, CounterJinx, CounterCharm, Untransfiguration, BindingMagicalContract, Vanishment
    }

    public enum SpellLight
    {
        None, Blue, IcyBlue, Red, Gold, Purple, Transparent, White, Green, Orange, Yellow, BrightBlue, Pink, Violet, BlueishWhite, Silver, Scarlet, Fire, FieryScarlet, Grey, DarkRed, Turquoise, PsychedelicTransparentWave, BrightYellow, BlackSmoke
    }
}
