// StatusEffect_OnKillHealToSelf.cs
// Registers the "On Kill Heal To Self" effect for the mod.
using System;
using WildfrostBirthday;

namespace WildfrostBirthday
{
    public static class StatusEffect_OnKillHealToSelf
    {
        /// <summary>
        /// Registers the "On Kill Heal To Self" effect.
        /// </summary>
        public static void Register(WildFamilyMod mod)
        {
            mod.AddStatusEffect<StatusEffectApplyXOnKill>(
                "On Kill Heal To Self",
                "Restore 2 on kill",
                data =>
                {
                    data.effectToApply = mod.Get<StatusEffectData>("Increase Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: true
            );
        }
    }
}
