using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_ApoliseWarMachineSpacecraft
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-warcraft";
            string spritePath = "companions/warcraft";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apolise War Machine Spacecraft")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(40, 2, 8)  // Scrap HP, ATK, Counter
                .WithFlavour("The Ultimate War Machine From Apolise Spacelines, Capable Of Destroying Anything In Its Path. However, It Is Very Dangerous If It Gets Destroyed.")
                .WithCardType("Leader")
               
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("When Destroyed Apply Damage To Allies", 10),
                        mod.SStack("Hit All Enemies", 1),
                        mod.SStack("ImmuneToSnow", 1),
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Hellbent"), 1),
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Fragile"), 1),
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Pigheaded"), 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}