using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_ApoliseRailgunSentry
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-railgunsentry";
            string spritePath = "companions/railgunsentry";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apolise Railgun Sentry")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 20, 10)  // Scrap HP, ATK, Counter
                .WithFlavour("A Super Powerful, Yet Expensive Sentry From Apolise Spacelines")
                .WithCardType("Clunker")
               
                .WithValue(100)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 5),
                        mod.SStack("When Card Played Destroy Random Junk In Hand", 1),
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Recycle"), 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}