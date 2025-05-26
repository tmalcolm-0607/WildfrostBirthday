// Registers the "Rejuvenation" status effect for the mod.
// Effect that restores health at the end of turn
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Dead;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Effects
{    // Custom status effect class that inherits from StatusEffectData to implement healing
    public class StatusEffectRejuvenationSimple : StatusEffectData
    {
        private bool subbed;
        private bool primed;
        
        // Override the init method to subscribe to end of turn events
        public override void Init()
        {
            base.OnTurnEnd += RestoreHealth;
            Events.OnPostProcessUnits += Prime;
            subbed = true;
            Debug.Log("[Rejuvenation] Status effect initialized");
        }
        
        public void OnDestroy()
        {
            Unsub();
        }

        private void Unsub()
        {
            if (subbed)
            {
                Events.OnPostProcessUnits -= Prime;
                subbed = false;
            }
        }

        private void Prime(Character character)
        {
            primed = true;
            Unsub();
        }

        public override bool RunTurnEndEvent(Entity entity)
        {
            if (primed && target != null && target.enabled)
            {
                return entity == target;
            }
            return false;
        }        // Restore health at the end of turn (following StatusEffectShroom pattern)
        public IEnumerator RestoreHealth(Entity entity)
        {
            Debug.Log($"[Rejuvenation] Triggering healing effect for {count} health");
              // Create a heal object (negative damage = healing) - follow base game pattern
            Hit heal = new Hit(GetDamager(), target, -count)
            {
                screenShake = 0.1f,
                damageType = "heal"  // Use simple damage type
            };
            
            // Process the healing
            yield return heal.Process();
            yield return Sequences.Wait(0.2f);
            
            // Decrement the effect by 1 after use (exactly like Shroom does)
            int amount = 1;
            Events.InvokeStatusEffectCountDown(this, ref amount);
            if (amount != 0)
            {
                yield return CountDown(entity, amount);
            }
        }
    }    public static class StatusEffect_Rejuvenation_Simple
    {
        public static void Register(WildFamilyMod mod)
        {
            // Make sure the keyword exists first
            Keywords.Keyword_Rejuvenation.Register(mod);
            
            Debug.Log("[Rejuvenation] Registering status effect");
            
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectRejuvenationSimple>("Rejuvenation")
                .WithText("Restore {0} health at the end of turn")
                .WithStackable(true)
                .WithCanBeBoosted(true)
                .WithOffensive(false)
                .WithVisible(true)
                .WithType("rejuvenation")  // Use custom type like Pokefrost
                .WithKeyword("rejuvenation")  // Link to keyword
                .WithIcon("status/rejuvenation.png")  // Try using the builder method for icon
                .SubscribeToAfterAllBuildEvent<StatusEffectRejuvenationSimple>(data => 
                {
                    Debug.Log("[Rejuvenation] Setting up status effect properties");
                    
                    // Set basic properties 
                    data.visible = true;
                    data.isStatus = true;
                    data.stackable = true;
                    data.canBeBoosted = true;
                    data.offensive = false;
                    data.doesDamage = false;
                    data.removeOnDiscard = true;
                    data.eventPriority = 0;
                    data.textInsert = "{a}";  // Format for displaying count
                    data.iconGroupName = "counter";  // Group with other counter icons like Snow/Shroom
                    
                    // Set target constraints - following base game patterns
                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintCanBeHit>(),
                        ScriptableObject.CreateInstance<TargetConstraintIsAlive>()
                    };
                });
            
            mod.assets.Add(builder);
            Debug.Log("[WildfrostBirthday] Successfully registered 'Rejuvenation' status effect");
        }
    }
}
