using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenHitApplyDemonizeToAttacker
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXWhenHit>(
                "When Hit Apply Demonize To Attacker",
                "When hit, apply 1 Demonize to the attacker",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Demonize");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                canBeBoosted: true
            );
        }
    }
}
