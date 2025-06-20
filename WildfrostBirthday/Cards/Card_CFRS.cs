using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_CFRS
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-cfrs";
            string spritePath = "companions/cfrs";
            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "C.F.R.S.")
                .SetSprites(spritePath + ".png", spritePath + "bg.png") // Adjust sprite paths as needed
                .SetStats(null, 2, 4) // Scrap, ATK, Counter
                .WithCardType("Clunker")
                .WithValue(60)
                .WithFlavour("A clunker equipped with Krunker's artillery.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 2)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Bombard 1", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}