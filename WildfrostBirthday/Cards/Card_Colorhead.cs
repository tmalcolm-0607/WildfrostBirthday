using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Colorhead
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-colorhead";
            string spritePath = "companions/colorhead";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Colorhead")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(3, 3, 5)  // HP, ATK, Counter
                .WithFlavour("A paintbrush with the power to shape its properties to one of its allies, Colorhead will copy the effects of a random ally before attacking.")
                .WithCardType("Friendly")
               
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Pre Trigger Copy Effects Of RandomAlly", 1),
                        
                    };
                    
                 
                });
            mod.assets.Add(companionBuilder);
        }
    }
}