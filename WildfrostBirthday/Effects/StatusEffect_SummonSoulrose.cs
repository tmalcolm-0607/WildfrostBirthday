// StatusEffect_SummonSoulrose.cs
// Registers the "Summon Soulrose" effect for the mod.
using System;

public static class StatusEffect_SummonSoulrose
{
    /// <summary>
    /// Registers the "Summon Soulrose" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        mod.AddCopiedStatusEffect<StatusEffectSummon>(
            "Summon Fallow", "Summon Soulrose",
            data =>
            {
                data.summonCard = mod.TryGet<CardData>("companion-soulrose");
            }
        );
    }
}
