using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenHitGainAttackToSelfNoPing
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnKill>(
                "When Hit Gain Attack To Self (No Ping)",
                "Gain 1 Attack On Hit",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Increase Attack");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: true
            );
        }
    }
}
