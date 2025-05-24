using System;
using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Card_Apricot
    {
        public static void Register(WildFamilyMod mod)
        {
            // First, register the phase transition status effect
            var phaseBuilder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectNextPhase>("ShellBossPhase2")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .WithType("nextphase")
                .SubscribeToAfterAllBuildEvent<StatusEffectNextPhase>(data =>
                {
                    data.preventDeath = true;
                    data.nextPhase = mod.TryGet<CardData>("apricot_2");
                    // Let the game handle the default transformation animation
                    data.animation = null;
                });

            mod.assets.Add(phaseBuilder);

            string cardId = "apricot";
            string spritePath = "enemies/apricot";

            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apricot")
                .SetSprites(spritePath + "shellboss.png", "bg.png")
                .SetStats(5, 5, 6)  // HP: 5, ATK: 5, Counter: 6
                .WithFlavour("A master of shell manipulation, turning defense into devastating offense.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("Shell", 10),
                        mod.SStack("ShellBossPhase2", 1),
                        mod.SStack("Bonus Damage Equal To Shell", 1)
                    };

                    // Set traits - Backline
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Backline", 1)
                    };
                });

            mod.assets.Add(builder);

            // Create Apricot Phase 2 variant
            string cardId2 = "apricot_2";
            
            var builder2 = new CardDataBuilder(mod)
                .CreateUnit(cardId2, "Apricot")
                .SetSprites(spritePath + "shellboss3.png", "bg.png")
                .SetStats(5, 1, 1)  // HP: 5, ATK: 1, Counter: 1
                .WithFlavour("Now focusing purely on defense, Apricot unleashes devastating area attacks.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("Shell", 25),
                        mod.SStack("Hit All Enemies", 1)
                    };

                    // Set traits - Frontline
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Frontline", 1)
                    };
                });

            mod.assets.Add(builder2);
        }
    }
}
