// Template file for cards/units using the direct builder pattern

using System.Collections.Generic;
using UnityEngine;
using Dead;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Template
    {
        public static void Register(WildFamilyMod mod)
        {
            // Helper method to get random number for dynamic sprite selection
            string getSpritePath(string basePath, string cardId)
            {
                if (cardId == "alison" || cardId == "caleb" || cardId == "lulu" || cardId == "poppy")
                {
                    int randomNumber = Dead.Random.Range(0, 3);
                    return basePath + randomNumber + ".png";
                }
                return basePath + ".png";
            }
            
            // COMPANION VERSION
            string cardId = "companion-template";
            string spritePath = "leaders/template";
            
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Template Card")
                .SetSprites(getSpritePath(spritePath, cardId), "bg.png")
                .SetStats(9, 3, 3)  // HP, ATK, Counter
                .WithFlavour("This is a template card")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Attack effects
                    data.attackEffects = new CardData.StatusEffectStacks[] { 
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect1"), 2)
                    };
                    
                    // Start with effects
                    data.startWithEffects = new CardData.StatusEffectStacks[] {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect2"), 1)
                    };
                });
                
            mod.assets.Add(companionBuilder);
            
            // LEADER VERSION (if needed)
            var leaderBuilder = new CardDataBuilder(mod)
                .CreateUnit("leader-template", "Template Card")
                .SetSprites(getSpritePath(spritePath, "template"), "bg.png")
                .SetStats(9, 3, 3)  // HP, ATK, Counter
                .WithFlavour("This is a template leader")
                .WithCardType("Leader")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Attack effects
                    data.attackEffects = new CardData.StatusEffectStacks[] { 
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect1"), 2)
                    };
                    
                    // Start with effects
                    data.startWithEffects = new CardData.StatusEffectStacks[] {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect2"), 1)
                    };
                })
                .FreeModify(data =>
                {                    // Leader-specific scripts
                    data.createScripts = new CardScript[]
                    {
                        CardScriptHelpers.GetGiveUpgradeScript(mod),
                        CardScriptHelpers.GetRandomHealthScript(-1, 3),
                        CardScriptHelpers.GetRandomDamageScript(0, 2),
                        CardScriptHelpers.GetRandomCounterScript(-1, 1)
                    };
                });
                
            mod.assets.Add(leaderBuilder);        }
    }
}
