using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Cassie
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "cassie";
            string spritePath = "leaders/cassie";
            
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit("companion-" + cardId, "Cassie")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(5, 0, 3)  // HP, ATK, Counter (attack reduced from 1 to 0)
                .WithFlavour("Joyful and chaotic, Cassie bounces through battle with ink and impulse.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("MultiHit", 2),
                        mod.SStack("On Turn Apply Ink To RandomEnemy", 1) // Ink reduced from 2 to 1
                    };
                    // Set traits
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Aimless"), 1)
                    };
                });
            mod.assets.Add(companionBuilder);
            
            // LEADER VERSION
            var leaderBuilder = new CardDataBuilder(mod)
                .CreateUnit("leader-" + cardId, "Cassie")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(5, 0, 3)  // HP, ATK, Counter (attack reduced from 1 to 0)
                .WithFlavour("Joyful and chaotic, Cassie bounces through battle with ink and impulse.")
                .WithCardType("Leader")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("MultiHit", 1),
                        mod.SStack("On Turn Apply Ink To RandomEnemy", 1) // Ink reduced from 2 to 1
                    };
                    // Set traits
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Aimless"), 1)
                    };
                    data.createScripts = new CardScript[]
                    {
                        CardScriptHelpers.GetGiveUpgradeScript(mod),
                        CardScriptHelpers.GetRandomHealthScript(-1, 3),
                        CardScriptHelpers.GetRandomDamageScript(0, 2),
                        CardScriptHelpers.GetRandomCounterScript(-1, 1)
                    };
                });
            mod.assets.Add(leaderBuilder);
        }
    }
}
