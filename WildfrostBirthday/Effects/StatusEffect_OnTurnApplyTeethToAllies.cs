using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnTurnApplyTeethToAllies
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnTurn>(
                "On Turn Apply Teeth To Allies",
                "While Active Teeth To Allies (Kaylee)",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Teeth");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                },
                canBeBoosted: true
            );
        }
    }
}
