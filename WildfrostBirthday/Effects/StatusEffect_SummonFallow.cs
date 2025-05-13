using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_SummonFallow
    {
        public static void Register(WildFamilyMod mod)
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
}
