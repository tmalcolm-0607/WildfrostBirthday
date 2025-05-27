using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Wallhead
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-wallhead";
            string spritePath = "companions/wallhead";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Wallhead", idleAnim: "GiantAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, null, 0)  // Scrap HP, ATK, Counter
                .WithFlavour("A wall that once placed, must stay in place.")
                .WithCardType("Clunker")
               
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 7)
                    };
                    data.traits = new List<CardData.TraitStacks>
                    {
                        mod.TStack("Unmovable", 1),  // Wallhead cannot be moved once placed
                        mod.TStack("Pigheaded", 1),  // Wallhead cannot be recalled
                        mod.TStack("Frontline", 1),  // Wallhead must be placed in the front
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}