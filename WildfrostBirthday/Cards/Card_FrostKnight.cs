using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;


namespace WildfrostBirthday.Cards
{
    /// <summary>
    /// Helper class for creating scriptable objects with initialization
    /// </summary>
    public class Scriptable<T> where T : ScriptableObject, new()
    {
        readonly Action<T> modifier;
        public Scriptable() { }
        public Scriptable(Action<T> modifier) { this.modifier = modifier; }
        public static implicit operator T(Scriptable<T> scriptable)
        {
            T result = ScriptableObject.CreateInstance<T>();
            scriptable.modifier?.Invoke(result);
            return result;
        }
    }

    public static class Card_FrostKnight
    {        
        public static void Register(WildFamilyMod mod)
        {
            // First, register the phase transition status effect
            var phaseBuilder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectNextPhase>("FrostBossPhase2")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .WithType("nextphase")
                .SubscribeToAfterAllBuildEvent<StatusEffectNextPhase>(data =>
                {
                    data.preventDeath = true;
                    data.nextPhase = mod.TryGet<CardData>("frost_knight_2");
                    // Let the game handle the default transformation animation
                    data.animation = null;
                });

            mod.assets.Add(phaseBuilder);            string cardId = "frost_knight";
            string spritePath = "enemies/frost_knight";

            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "The Frost Knight")
                .SetSprites(spritePath + "frostboss.png", "bg.png")
                .SetStats(40, 10, 6)  // HP: 40, ATK: 10, Counter: 6
                .WithFlavour("An ancient warrior encased in glacial armor, commanding the very essence of winter itself.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with ImmuneToSnow 1
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("FrostBossPhase2", 1)
                    };

                    // Attack effect: Apply 10 Frost
                    data.attackEffects = new[] {
                        mod.SStack("Frost", 10)
                    };
                });

            mod.assets.Add(builder);

            // Create Frost Knight 2 variant
            string cardId2 = "frost_knight_2";            var builder2 = new CardDataBuilder(mod)
                .CreateUnit(cardId2, "The Frost Knight")
                .SetSprites(spritePath + "frostboss2.png", "bg.png")
                .SetStats(60, 2, 3)  // HP: 60, ATK: 2, Counter: 3
                .WithFlavour("A more agile variant of the Frost Knight, darting around with rapid strikes.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects: ImmuneToSnow 1, MultiHit 3
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("MultiHit", 3)
                    };

                    // Set traits - Aimless
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Aimless", 1)
                    };
                });

            mod.assets.Add(builder2);
        }
    }
}
