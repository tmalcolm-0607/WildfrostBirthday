using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Kaylee
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "kaylee";
            string spritePath = "leaders/kaylee";

            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit("companion-" + cardId, "Kaylee")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(7, 4, 7)  // HP, ATK, Counter
                .WithFlavour("Sharp-witted and sharper-fanged, Kaylee boosts all allies' bite.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Deployed Apply Teeth To Self", 4),
                        mod.SStack("On Turn Apply Teeth To Allies", 2)
                    };
                    // Attach dynamic sprite change script if needed in the future
                    // Example for reference:
                    // data.createScripts = new CardScript[] { new CardScriptChangeMainOnCounter("images/companions/kaylee0.png", "images/companions/kaylee1.png", "images/companions/kaylee2.png", "images/companions/kaylee3.png") };
                });

            mod.assets.Add(companionBuilder);
            
            // LEADER VERSION
            var leaderBuilder = new CardDataBuilder(mod)
                .CreateUnit("leader-" + cardId, "Kaylee")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(7, 4, 7)  // HP, ATK, Counter
                .WithFlavour("Sharp-witted and sharper-fanged, Kaylee boosts all allies' bite.")
                .WithCardType("Leader")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Deployed Apply Teeth To Self", 4),
                        mod.SStack("On Turn Apply Teeth To Allies", 2)
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
