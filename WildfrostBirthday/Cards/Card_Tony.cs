using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Tony
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "tony";
            string spritePath = "leaders/tony";
            
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit("companion-" + cardId, "Tony")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(8, 2, 4)  // HP, ATK, Counter
                .WithFlavour("Summon Soulrose")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Deployed Summon Soulrose", 1),
                        mod.SStack("On Turn Summon Soulrose", 1)
                    };
                });
                
            mod.assets.Add(companionBuilder);
            
            // LEADER VERSION
            var leaderBuilder = new CardDataBuilder(mod)
                .CreateUnit("leader-" + cardId, "Tony")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(8, 2, 4)  // HP, ATK, Counter
                .WithFlavour("Summon Soulrose")
                .WithCardType("Leader")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Deployed Summon Soulrose", 1),
                        mod.SStack("On Turn Summon Soulrose", 1)
                    };
                    
                    // Leader-specific scripts
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
