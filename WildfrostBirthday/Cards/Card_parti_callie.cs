using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_PartiCallie
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-parti_callie";
            string spritePath = "companions/parti_callie";
            
            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Parti-Callie Accele-Rella")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 4, 4)  // Scrap HP, ATK, Counter
                .WithFlavour("A Perfected machine, Parti-Callie Accele-Rella is the ultimate creation of the Madfamily, constantly charging power with each trigger.")
                .WithCardType("Companion")
               
                .WithValue(999999) // Set a high value to prevent it from being used in normal gameplay
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 10),
                        mod.SStack("Shell", 30),
                        mod.SStack("On Turn Apply Attack To Self", 2) // Gain attack at the start of each turn
                    };
                    data.attackEffects = new[]
                    {
                        mod.SStack("Overload", 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}