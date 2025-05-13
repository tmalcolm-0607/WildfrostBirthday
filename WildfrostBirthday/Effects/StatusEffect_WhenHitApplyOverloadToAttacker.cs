using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenHitApplyOverloadToAttacker
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnKill>(
                "When Hit Apply Overload To Attacker",
                "When Hit Apply Overload",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Overload");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                canBeBoosted: true
            );
        }
    }
}
