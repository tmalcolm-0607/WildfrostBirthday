// StatusEffect_InstantSummonSoulrose.cs
// Registers the "Instant Summon Soulrose" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_InstantSummonSoulrose
{
    /// <summary>
    /// Registers the "Instant Summon Soulrose" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectInstantSummon>("Instant Summon Soulrose")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .WithOffensive(false)      // As an attack effect, this is treated as a buff
            .WithMakesOffensive(false) // As a starting effect, its entity should target allies
            .WithDoesDamage(false)     // Its entity cannot kill with this effect, eg for Bling Charm
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummon>(data =>
            {
                data.eventPriority = 99999;
                data.targetSummon = mod.TryGet<StatusEffectSummon>("Summon Soulrose");
            });
        mod.assets.Add(builder);
    }
}
