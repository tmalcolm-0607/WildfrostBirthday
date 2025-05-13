using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_SummonBeepopWisp
    {
        public static void Register(WildFamilyMod mod)
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
}
