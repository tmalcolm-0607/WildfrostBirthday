using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_FrostMoonApplyFrostburnOnAttack
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnTurn>(
                "FrostMoon Apply Frostburn On Attack",
                "On attack, apply 5 Frostburn",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Frostburn");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                }
            );
        }
    }
}
