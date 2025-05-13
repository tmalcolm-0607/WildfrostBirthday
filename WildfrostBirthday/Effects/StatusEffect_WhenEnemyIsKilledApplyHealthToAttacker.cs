// StatusEffect_WhenEnemyIsKilledApplyHealthToAttacker.cs
// Registers the "When Enemy Is Killed Apply Health To Attacker" effect for the mod.
using System;
using WildfrostBirthday;

namespace WildfrostBirthday
{
    public static class StatusEffect_WhenEnemyIsKilledApplyHealthToAttacker
    {
        /// <summary>
        /// Registers the "When Enemy Is Killed Apply Health To Attacker" effect.
        /// </summary>
        public static void Register(WildFamilyMod mod)
        {
            mod.AddCopiedStatusEffect<StatusEffectApplyXWhenUnitIsKilled>(
                "When Enemy Is Killed Apply Shell To Attacker",
                "When Enemy Is Killed Apply Health To Attacker",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Increase Max Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                text: "When an enemy is killed, apply <{a}><keyword=health> to the attacker",
                textInsert: "<keyword=health>"
            );
        }
    }
}
