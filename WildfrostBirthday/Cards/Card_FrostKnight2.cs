using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_FrostKnight2
    {        
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "frost_knight_2";
            string spritePath = "enemies/frost_knight_2";

            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "The Frost Knight", idleAnim: "HeartbeatAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(60, 2, 3)  // HP: 60, ATK: 2, Counter: 3
                .WithFlavour("Revealing his true power, the Frost Knight unleashes a blizzard upon his foes, striking with furious vengeance.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects: ImmuneToSnow 1, MultiHit 3
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("MultiHit", 3)
                    };

                    // Set traits - Aimless
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Aimless", 1)
                    };
                });

            mod.assets.Add(builder);
        }
    }
}
