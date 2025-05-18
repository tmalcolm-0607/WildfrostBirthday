using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Bwob
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-bwob";
            string spritePath = "companions/bwob";
            
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Bwob")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(20, 1, 6)  // HP, ATK, Counter
                .WithFlavour("When X health lost, split 4")
                .WithCardType("Friendly")
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When X Health Lost Split", 4)
                    };
                     data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Fragile", 1),
                        mod.TStack("Pigheaded", 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}
