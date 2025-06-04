using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_ApoliseMiniSentry
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-minisentry";
            string spritePath = "companions/minisentry";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apolise Mini Sentry")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 2, 2)  // Scrap HP, ATK, Counter
                .WithFlavour("A Miniature Sentry From Apolise Spacelines")
                .WithCardType("Clunker")
               
                .WithValue(30)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 1)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Spark"), 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}