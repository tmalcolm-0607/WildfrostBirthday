// StatusEffect_SummonWisp.cs
// Registers the "Summon Wisp" effect for the mod.
using System;

public static class StatusEffect_SummonWisp
{
    /// <summary>
    /// Registers the "Summon Wisp" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        mod.AddCopiedStatusEffect<StatusEffectSummon>(
            "Summon Beepop", "Summon Wisp",
            data =>
            {
                data.summonCard = mod.TryGet<CardData>("companion-wisp");
            },
            text: "{0}",
            textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-wisp>"
        );
    }
}
