using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Hazefairy
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-hazefairy";
            string spritePath = "companions/hazefairy";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Hazefairy", idleAnim: "FlyAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(1, 0, 4)  // HP, ATK, Counter
                .WithFlavour("A small, weak, but feisty fairy who sprays fog in the face of her foes.")
                .WithCardType("Friendly")
               
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Weakness", 1)
                    };
                    data.attackEffects = new[]
                    {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Haze"), 1)
                    };
                    data.traits = new List<CardData.TraitStacks>
                    {
                        mod.TStack("Aimless", 1),
                        
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}