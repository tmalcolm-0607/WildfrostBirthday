using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_CollectBlingOnTrigger
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnTurn>(
                "Collect Bling On Trigger",
                "Gain 1 Bling when triggered",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Bling");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: false
            );
        }
    }
}
