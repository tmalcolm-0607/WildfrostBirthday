using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenAllyIsHitApplyFrostToAttacker
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXWhenAllyIsHit>(
                "When Ally is Hit Apply Frost To Attacker",
                "When ally is hit, apply 2 Frost to the attacker",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Frost");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                canBeBoosted: true
            );
        }
    }
}
