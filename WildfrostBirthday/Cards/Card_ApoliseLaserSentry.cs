using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_ApoliseLaserSentry
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-lasersentry";
            string spritePath = "companions/lasersentry";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apolise Laser Sentry")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 3, 5)  // Scrap HP, ATK, Counter
                .WithFlavour("A Laser Weapon Added To The Standard Sentry From Apolise Spacelines")
                .WithCardType("Clunker")
               
                .WithValue(70)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 2)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Barrage"), 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}