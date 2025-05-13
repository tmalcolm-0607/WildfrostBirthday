// StatusEffect_WhenDeployedSummonSoulrose.cs
// Registers the "When Deployed Summon Soulrose" effect for the mod.
using System;

public static class StatusEffect_WhenDeployedSummonSoulrose
{
    /// <summary>
    /// Registers the "When Deployed Summon Soulrose" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        mod.AddCopiedStatusEffect<StatusEffectApplyXWhenDeployed>(
            "When Deployed Summon Wowee", "When Deployed Summon Soulrose",
            data =>
            {
                data.effectToApply = mod.TryGet<StatusEffectData>("Summon Soulrose");
            }
        );
    }
}
