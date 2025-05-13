using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_InstantSummonFallow
    {
        public static void Register(WildFamilyMod mod)
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
}
