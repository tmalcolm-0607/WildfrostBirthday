// Registers the "Rejuvenation" status effect for the mod.
// Effect that restores health at the end of turn
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Dead;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Effects
{
    // Custom status effect class that mimics Shroom/Overshroom but heals instead of damages
    public class StatusEffectRejuvenationSimple : StatusEffectData
    {
        public bool subbed;
        public bool primed;

        public override void Init()
        {
            base.OnTurnEnd += Heal;
            Events.OnPostProcessUnits += Prime;
            subbed = true;
        }

        public void OnDestroy()
        {
            Unsub();
        }

        public void Unsub()
        {
            if (subbed)
            {
                Events.OnPostProcessUnits -= Prime;
                subbed = false;
            }
        }

        public void Prime(Character character)
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
        }

        // Heals at the end of turn, decrements stacks (mirrors Shroom/Overshroom logic)
        public IEnumerator Heal(Entity entity)
        {
            if (!this || !target || !target.alive)
                yield break;

            // Heal for the current stack count
            Hit heal = new Hit(GetDamager(), target, -count)
            {
                screenShake = 0.1f,
                damageType = "heal"
            };
            yield return heal.Process();
            yield return Sequences.Wait(0.2f);

            // Decrement the effect by 1 after use (exactly like Shroom)
            int amount = 1;
            Events.InvokeStatusEffectCountDown(this, ref amount);
            if (amount != 0)
            {
                yield return CountDown(entity, amount);
            }
        }
    }

    public static class StatusEffect_Rejuvenation_Simple
    {
        public static void Register(WildFamilyMod mod)
        {
            // Make sure the keyword exists first
            Keywords.Keyword_Rejuvenation.Register(mod);

            Debug.Log("[Rejuvenation] Registering status effect");

            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectRejuvenationSimple>("Rejuvenation")
                .WithText("Restore {0}")
                .WithTextInsert("{a} <keyword=rejuvenation>") // {a} for count, keyword in text
                .WithStackable(true)
                .WithCanBeBoosted(true)
                .WithOffensive(false)
                .WithVisible(true)
                .WithType("rejuvenation")
                .WithIcon("status/rejuvenation.png")
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
                    data.iconGroupName = "health";  // Group with other health icons

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
