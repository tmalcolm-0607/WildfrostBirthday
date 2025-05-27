using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Peppernote
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-peppernote";
            string spritePath = "companions/peppernote";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Peppernote", idleAnim: "Heartbeat2AnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(5, null, 4)  // HP, ATK, Counter
                .WithFlavour("A music note that plays a spicy tune, boosting the power of its allies to raise them to higher tempos.")
                .WithCardType("Friendly")
               
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    
                    data.startWithEffects = new[]
                    {
                        mod.SStack("On Card Played Apply Spice To RandomAlly", 1),
                        mod.SStack("MultiHit", 3)
                    };
                    
                    
                });
            mod.assets.Add(companionBuilder);
        }
    }
}