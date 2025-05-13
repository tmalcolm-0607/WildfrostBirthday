using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnTurnApplyInkToRandomEnemy
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnTurn>(
                "On Turn Apply Ink To RandomEnemy",
                "On hit, apply 2 Ink to the target",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Null");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                },
                canBeBoosted: true
            );
        }
    }
}
