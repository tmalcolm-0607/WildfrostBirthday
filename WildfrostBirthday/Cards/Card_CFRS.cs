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
                .SetSprites(spritePath + ".png", spritePath + "_bg.png") // Adjust sprite paths as needed
                .SetStats(null, 1, 2) // Scrap, ATK, Counter
                .WithCardType("Clunker")
                
                .WithFlavour("A clunker equipped with Krunker's artillery.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 2)
                    };
                  data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Bombard 1"), 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}