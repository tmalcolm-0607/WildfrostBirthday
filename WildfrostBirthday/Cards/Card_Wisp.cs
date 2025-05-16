using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Wisp
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-wisp";
            string spritePath = "companions/wisp";
            
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Wisp")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(5, 4, 6)  // HP, ATK, Counter
                .WithFlavour("When an enemy is killed, apply 2 health to the attacker")
                .WithCardType("Friendly")
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Enemy Is Killed Apply Health To Attacker", 2) // Health reduced from 4 to 2
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}
