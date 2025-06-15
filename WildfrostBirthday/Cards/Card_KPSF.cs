using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_KPSF
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-kpsf";
            string spritePath = "companions/kpsf";
            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "K.P.S.F.")
                .SetSprites(spritePath + ".png", spritePath + "bg.png") // Adjust sprite paths as needed
                .SetStats(null, 3, 5) // Scrap, ATK, Counter
                .WithCardType("Clunker")
                .WithValue(60)
                .WithFlavour("A clunker equipped with Krunker's artillery.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 2),
                        mod.SStack("When Hit Reduce Attack To Self", 3),
                        mod.SStack("On Turn Apply Attack To Self", 1)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Aimless", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}