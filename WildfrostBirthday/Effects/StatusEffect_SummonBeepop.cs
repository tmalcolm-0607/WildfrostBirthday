using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_SummonBeepop
    {
        public static void Register(WildFamilyMod mod)
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
}
