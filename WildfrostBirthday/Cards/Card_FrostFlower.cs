using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Frostflower
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-frostflower";
            string spritePath = "companions/frostflower";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Frostflower")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, null, 0)  // Scrap HP, ATK, Counter
                .WithFlavour("A Flower that blooms in the coldest of winters, bringing a those who touch it a sense of weakness.")
                .WithCardType("Clunker")
                
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 3),
                        mod.SStack("When Hit Apply Frost To Attacker", 2),
                    };
                    
                });
            mod.assets.Add(companionBuilder);
        }
    }
}