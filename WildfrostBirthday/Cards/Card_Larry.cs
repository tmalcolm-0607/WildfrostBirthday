using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Larry
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-larry";
            string spritePath = "companions/larry";
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Larry")
                .SetSprites(spritePath + ".png", "bg.png") // Adjust sprite paths as needed
                .SetStats(3, 1, 7) // HP, ATK, Counter
                .WithCardType("Friendly")                .WithFlavour("A knitted friend of cassie, always ready to help by healing allies when hit.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Use the helper methods to get status effect stacks and trait stacks
                    data.startWithEffects = new[] {
                        mod.SStack("On Turn Heal Allies", 1),
                        mod.SStack("Scrap", 2)
                    };
                    
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Smackback", 1),
                        
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}