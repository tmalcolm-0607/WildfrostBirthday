using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_FrostMoonIncreaseMaxCounter
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXWhenDeployed>(
                "FrostMoon Increase Max Counter",
                "When deployed, gain +2 counter",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Increase Max Counter");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                }
            );
        }
    }
}
