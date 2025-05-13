using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenDeployedApplyTeethToSelf
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXWhenDeployed>(
                "When Deployed Apply Teeth To Self",
                "Teeth",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Teeth");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: true
            );
        }
    }
}
