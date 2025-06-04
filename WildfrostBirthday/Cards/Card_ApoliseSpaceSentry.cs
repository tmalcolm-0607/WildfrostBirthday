using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_ApoliseSpaceSentry
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-spacesentry";
            string spritePath = "companions/spacesentry";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apolise Space Sentry")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 3, 3)  // Scrap HP, ATK, Counter
                .WithFlavour("A Standard Sentry From Apolise Spacelines")
                .WithCardType("Clunker")
               
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 2)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Aimless"), 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}