using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Blocko
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-blocko";
            string spritePath = "companions/blocko";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Blocko")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(1, 7, 6)  // HP, ATK, Counter
                .WithFlavour("A sturdy block that can take five hits and can hit foes with full-body assault with the powers of ice.")
                .WithCardType("Friendly")
               
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Block", 5),
                        mod.SStack("On Kill Apply Block To Self", 1) // This trait will allow Blocko to deal damage equal to its block amount
                    };
                    
                 
                });
            mod.assets.Add(companionBuilder);
        }
    }
}