using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Soulrose
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-soulrose";
            string spritePath = "companions/soulrose";
            
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Soulrose")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(1, null, 0)  // HP, ATK, Counter
                .WithFlavour("When destroyed, add +1 health to all allies")
                .WithCardType("Friendly")
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Destroyed Add Health To Allies", 1)
                    };
                });
                
            mod.assets.Add(companionBuilder);
        }
    }
}
