using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Lulu
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "lulu";
            string spritePath = "companions/lulu";
            
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Lulu")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(6, 2, 3)  // HP, ATK, Counter
                .WithFlavour("Lulu defends her family with snowy retaliation.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Ally is Hit Apply Frost To Attacker", 2)
                    };
                });
                
            mod.assets.Add(companionBuilder);
        }
    }
}
