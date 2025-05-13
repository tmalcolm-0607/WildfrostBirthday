// StatusEffect_OnTurnSummonSoulrose.cs
// Registers the "On Turn Summon Soulrose" effect for the mod.
using System;

public static class StatusEffect_OnTurnSummonSoulrose
{
    /// <summary>
    /// Registers the "On Turn Summon Soulrose" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        mod.AddCopiedStatusEffect<StatusEffectSummon>(
            "Summon Beepop", "On Turn Summon Soulrose",
            data =>
            {
                data.summonCard = mod.TryGet<CardData>("companion-soulrose");
            },
            text: "{0}",
            textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-soulrose>"
        );
    }
}
