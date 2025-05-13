// StatusEffect_WhenDestroyedAddHealthToAllies.cs
// Registers the "When Destroyed Add Health To Allies" effect for the mod.
using System;
using WildfrostBirthday;

namespace WildfrostBirthday
{
    public static class StatusEffect_WhenDestroyedAddHealthToAllies
    {
        /// <summary>
        /// Registers the "When Destroyed Add Health To Allies" effect.
        /// </summary>
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXWhenDestroyed>(
                "When Destroyed Add Health To Allies",
                "When destroyed, add 1 to all allies (Max)",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Increase Max Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                },
                type: "Increase Max Health",
                canBeBoosted: true
            );
        }
    }
}
