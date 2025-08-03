using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_PressureConversionCube
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-pressurecube";
            string spritePath = "companions/pressurecube";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "P. C. C.")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 0, 64)  // Scrap HP, ATK, Counter
                .WithFlavour("A device that converts outgoing trauma into internal pressure.")
                .WithCardType("Clunker")
                .WithText("'Pressure Conversion Cube' for short")
                .WithValue(100) // A high price so it would be difficult to obtain outside of chests
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                     {
                        mod.SStack("Scrap", 9999), // Start with a lot of scrap so it cannot be killed by enemies
                        mod.SStack("Destroy Self After Turn", 1), // Destroys self after turn
                        mod.SStack("When Hit Reduce Counter To Self", 1),
                        mod.SStack("When Hit Gain Attack To Self (No Ping)", 1) // Gain attack at the start of each turn
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}