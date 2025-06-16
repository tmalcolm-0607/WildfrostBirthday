using System;
using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Card_Apricot2
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "apricot_2";
            string spritePath = "enemies/apricot_2";
            
            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apricot", idleAnim: "SwayAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(5, 1, 1)  // HP: 5, ATK: 1, Counter: 1
                .WithFlavour("Now freed from her prison, Apricot dances around and strikes with precision on all of her foes.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("Shell", 25),
                        mod.SStack("Hit All Enemies", 1)
                    };

                    // Set traits - Spark
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Spark", 1),
                        mod.TStack("Frontline", 1)
                    };
                });

            mod.assets.Add(builder);
        }
    }
}
