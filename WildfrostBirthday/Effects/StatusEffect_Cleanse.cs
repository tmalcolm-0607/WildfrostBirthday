// StatusEffect_Cleanse.cs
// Registers the "Cleanse" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
public static class StatusEffect_Cleanse
{
    /// <summary>
    /// Registers the "Cleanse" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectInstantCleanse>("Cleanse")
            .WithText("{0}", SystemLanguage.English)
            .WithText("{0}", SystemLanguage.ChineseSimplified)
            .WithText("{0}", SystemLanguage.ChineseTraditional)
            .WithText("{0}", SystemLanguage.Korean)
            .WithText("{0}", SystemLanguage.Japanese)
            .WithTextInsert("<keyword=cleanse>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .WithOffensive(false)      // As an attack effect, this is treated as a buff
            .WithMakesOffensive(false) // As a starting effect, its entity should target allies
            .WithDoesDamage(false)     // Its entity cannot kill with this effect, eg for Bling Charm
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantCleanse>(data =>
            {
                data.eventPriority = 10; // Setting a priority for the effect
                // Add any other necessary configuration
            });
              mod.assets.Add(builder);
    }
}
}
