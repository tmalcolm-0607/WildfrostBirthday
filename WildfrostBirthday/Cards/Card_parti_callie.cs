using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_PartiCallie
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-parti_callie";
            string spritePath = "companions/parti_callie";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Parti-Callie Accele-Rella")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 4, 2)  // Scrap HP, ATK, Counter
                .WithFlavour("A perfected machine, Parti-Callie Accele-Rella stores power over time, unleashing devastating attacks when fully charged.")
                .WithCardType("Friendly")
               
                .WithValue(9999) //Cannot be bought, only found in the chests as an extremely rare item
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 10),
                        mod.SStack("Shell", 30),
                        mod.SStack("On Turn Apply Attack To Self", 2) // Gain attack at the start of each turn
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Aimless"), 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}