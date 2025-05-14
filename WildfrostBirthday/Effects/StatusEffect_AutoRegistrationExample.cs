// Example status effect that demonstrates automatic registration
using System.Collections.Generic;
using UnityEngine;
using Dead;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_AutoRegistrationExample
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the status effect builder directly
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnTurn>("Auto-Registration Example")
                .WithText("On turn, apply 1 Spice to all allies")
                .WithType("Support")
                .WithCanBeBoosted(true)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
                {                    // Configure the effect's behavior
                    data.effectToApply = mod.TryGet<StatusEffectData>("Spice");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                    // The amount is determined by the status effect itself
                });
            mod.assets.Add(builder);
            
            Debug.Log("[MadHouse Family Tribe] Registered AutoRegistrationExample effect through automated system");
        }
    }
}
