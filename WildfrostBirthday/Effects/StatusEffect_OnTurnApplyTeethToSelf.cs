using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnTurnApplyTeethToSelf
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnTurn>(
                "On Turn Apply Teeth To Self",
                "While Active Teeth To Allies (Kaylee)",
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
