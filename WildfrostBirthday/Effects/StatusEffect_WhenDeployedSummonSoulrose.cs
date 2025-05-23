// StatusEffect_WhenDeployedSummonSoulrose.cs
// Registers the "When Deployed Summon Soulrose" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_WhenDeployedSummonSoulrose
{
    /// <summary>
    /// Registers the "When Deployed Summon Soulrose" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectApplyXWhenDeployed>("When Deployed Summon Soulrose")
            .WithText("Summon", SystemLanguage.English)
            .WithText("Summon", SystemLanguage.ChineseSimplified)
            .WithText("Summon", SystemLanguage.ChineseTraditional)
            .WithText("Summon", SystemLanguage.Korean)
            .WithText("Summon", SystemLanguage.Japanese)
            .WithTextInsert("<card=madfamilymod.wildfrost.madhouse.companion-soulrose>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .WithOffensive(false)      // As an attack effect, this is treated as a buff
            .WithMakesOffensive(false) // As a starting effect, its entity should target allies
            .WithDoesDamage(false)     // Its entity cannot kill with this effect, eg for Bling Charm
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(data =>
            {
                data.eventPriority = 99999;
                data.effectToApply = mod.TryGet<StatusEffectInstantSummon>("Instant Summon Soulrose");
                data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                data.waitForAnimationEnd = true;
                data.queue = true;
            });
        mod.assets.Add(builder);
    }
}