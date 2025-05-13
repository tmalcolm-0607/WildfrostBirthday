// StatusEffect_InstantSummonSoulrose.cs
// Registers the "Instant Summon Soulrose" effect for the mod.
using System;

public static class StatusEffect_InstantSummonSoulrose
{
    /// <summary>
    /// Registers the "Instant Summon Soulrose" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        mod.AddCopiedStatusEffect<StatusEffectInstantSummon>(
            "Instant Summon Fallow", "Instant Summon Soulrose",
            data =>
            {
                data.targetSummon = mod.TryGet<StatusEffectData>("Summon Soulrose") as StatusEffectSummon;
            }
        );
    }
}
