using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnTurnAddAttackToAllies
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddCopiedStatusEffect<StatusEffectApplyXOnTurn>(
                "On Turn Add Attack To Allies", "On Turn Add Attack To Self",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Increase Attack");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                }
            );
        }
    }
}
