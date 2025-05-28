using System;
using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Card_Dreadnought
    {
       

        public static void Register(WildFamilyMod mod)
        {

            string cardId = "companion-dreadnought";
            string spritePath = "companions/dreadnought";

            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Dreadnought", idleAnim: "GiantAnimationProfile")
                .SetSprites( "companions/dreadnought.png", "companions/dreadnought_bg.png")
                .SetStats(null, 10, 8)  // HP: none, ATK: 10, Counter: 8
                .WithFlavour("Dreadnought is a very deadly clunker with the most powerful weapon in the wildfrost. It must require heavy maintenance and upkeep.")
                
                .WithCardType("BossSmall")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    data.startWithEffects = new[] {
                        mod.SStack("Hit All Enemies", 1),
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("Scrap", 46),
                        mod.SStack("On Card Played Lose Scrap To Self", 9),
                        mod.SStack("On Card Played Destroy All Junk In Hand And Draw For Each", 0),
                        mod.SStack("Pre Turn Take Gold", 20),
                    };

                    // Set traits - Backline
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Pigheaded", 1),
                        mod.TStack("Recycle", 4), // Dreadnought must need 4 Junk in your hand to be able to attack!
                    };
                });
            mod.assets.Add(builder);
        }
    }
}