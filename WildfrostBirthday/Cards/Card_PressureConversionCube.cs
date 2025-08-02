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
            string cardId = "companion-pressure_conversion_cube";
            string spritePath = "companions/pressure_conversion_cube";

            var companionBuilder = new CardDataBuilder(mod)
                .CreateItem(cardId, "Pressure Conversion Cube")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(64, 0, 0)  // Scrap HP, ATK, Counter
                .WithFlavour("A device that converts outgoing trauma into internal pressure.")
                .WithCardType("clunker")
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("When Spice X Applied To Self Trigger To Self", 60), //This will trigger the effect when 60 spice is applied
                        mod.SStack("Destroy Self After Turn", 1), // Destroys self after turn
                        mod.SStack("When Health Lost Apply Equal Spice To Self", 1) // Gain attack at the start of each turn
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}