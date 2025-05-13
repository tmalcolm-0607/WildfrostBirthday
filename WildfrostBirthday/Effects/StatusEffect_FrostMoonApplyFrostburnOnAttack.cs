using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_FrostMoonApplyFrostburnOnAttack
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnTurn>(
                "FrostMoon Apply Frost On Attack",
                "On attack, apply 5 Frost",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Frost");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                }
            );
        }
    }
}
